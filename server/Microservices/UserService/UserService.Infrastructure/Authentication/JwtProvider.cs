using System.Security.Claims;
using System.Text;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace UserService.Infrastructure;

  public class JwtProvider : IJwtProvider
    { 
        private readonly JwtOptions _options;
 
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
 
    public (string accessToken, string refreshToken) Generate(UserModel user)
    {
         // Генерация Access токена
        var accessToken = GenerateToken(user, _options.AccessTokenExpiresMinutes);
     
         // Генерация Refresh токена
        var refreshToken = GenerateToken(user, _options.RefreshTokenExpiresDays, isRefreshToken: true);
     
        return (accessToken, refreshToken);
    }
 
 public string GenerateAccess(UserModel user)
 {
     // Генерация Access токена
     var accessToken = GenerateToken(user, _options.AccessTokenExpiresMinutes);
 
     // Генерация Refresh токена
 
     return accessToken;
 }
 
 
 private string GenerateToken(UserModel user, double expiresIn, bool isRefreshToken = false)
 {
     var claims = new List<Claim>
     {
         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
         new Claim(ClaimTypes.Name, user.Name),
         new Claim(ClaimTypes.Email, user.Email),
     };
 
     if (!isRefreshToken)
     {
         claims.Add(new Claim(ClaimTypes.Role, user.Role));
     }
     else
     {
         claims.Add(new Claim("isRefreshToken", "true"));
 
     }
 
     var signingCredentials = new SigningCredentials(
         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
         SecurityAlgorithms.HmacSha256
     );
 
     var token = new JwtSecurityToken(
         claims: claims,
         expires: DateTime.UtcNow.AddMinutes(expiresIn),
         signingCredentials: signingCredentials
     );
 
     return new JwtSecurityTokenHandler().WriteToken(token);
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
             ClockSkew = TimeSpan.Zero 
         };
 
         var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
 
         var isRefreshTokenClaim = principal.Claims.FirstOrDefault(c => c.Type == "isRefreshToken");
         if (isRefreshTokenClaim == null && isRefreshTokenClaim.Value != "true")
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
 
 
 
    }