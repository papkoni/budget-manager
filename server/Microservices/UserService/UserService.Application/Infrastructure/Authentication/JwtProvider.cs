using System.Security.Claims;
using System.Text;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using UserService.Application.DTO;
using UserService.Application.Extensions;

namespace UserService.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public TokensDTO GenerateTokens(UserModel user)
    {
        
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user.Id);
        return new TokensDTO(accessToken, refreshToken);
    }

    public string GenerateAccessToken(UserModel user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),  
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
        };

        return GenerateJwtToken(claims, _options.AccessTokenExpiresMinutes);
    }

    public string GenerateRefreshToken(Guid userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("isRefreshToken", "true"),
        };

        return GenerateJwtToken(claims, _options.RefreshTokenExpiresMinutes);
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims, double expiresIn)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiresIn),
            SigningCredentials = credentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public int GetRefreshTokenExpirationMinutes()
    {
        return _options.RefreshTokenExpiresMinutes;
    }
    
    public bool ValidateRefreshToken(string refreshToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }
            };

            var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

            var isRefreshTokenClaim = principal.Claims.FirstOrDefault(c => c.Type == "isRefreshToken");
            if (isRefreshTokenClaim?.Value != "true")
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
            return false;
        }
    }
    
    public Guid GetUserIdFromRefreshToken(string refreshToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.ReadJwtToken(refreshToken);

            var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("UserId claim not found in the refresh token.");
            }

            return Guid.Parse(userIdClaim.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to extract userId from refresh token: {ex.Message}");
            throw new Exception("Error extracting userId from refresh token.");
        }
    }

}
