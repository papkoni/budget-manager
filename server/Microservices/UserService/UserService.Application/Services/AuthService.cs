using UserService.Application.DTO;
using UserService.Application.Enums;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Models;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;

namespace UserService.Application.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    public AuthService(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IJwtProvider jwtProvider, 
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    
    public async Task<TokensDTO> RefreshTokensAsync(string refreshToken)
    {
        var isValidToken = _jwtProvider.ValidateRefreshToken(refreshToken);
        if (!isValidToken)
        {
            throw new Exception("try to login");
        }

        var extractedUserId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);
        var foundUser = await _userRepository.GetByIdAsync(extractedUserId);
        if (foundUser == null)
        {
            throw new Exception("try to login");
        }
        
        var tokens = _jwtProvider.GenerateTokens(foundUser);
        await _refreshTokenRepository.UpdateTokenAsync( //TODO: ADD CHECK
            foundUser.RefreshToken?.Id,
            tokens.RefreshToken, 
            _jwtProvider.GetRefreshTokenExpirationMinutes() );
        
        return tokens;
    }
    
    public async Task<TokensDTO> RegisterAsync(string name, string password, string email)
    {
    
        var foundUser = await _userRepository.GetByEmailAsync(email);
        if (foundUser != null)
        {
            throw new Exception("User already exists");
        }
    
        var hashedPassword = _passwordHasher.Generate(password);
        Console.WriteLine($"Checking password: {hashedPassword}");

        var user = new UserModel(Guid.NewGuid(), name, hashedPassword, email, Role.User);
        
        var tokens = _jwtProvider.GenerateTokens(user);
        var refreshToken = new RefreshTokenModel(Guid.NewGuid(), tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes(), user.Id);
        
        await _userRepository.AddAsync(user);

        await _refreshTokenRepository.AddAsync(refreshToken);
        return tokens;
    }
    
    
    
    public async Task<TokensDTO> LoginAsync(string email, string password)
    {
        var foundUser = await _userRepository.GetByEmailAsync(email);

        if (foundUser == null)
        {
            throw new Exception();
        }
        
        var isVerified = _passwordHasher.Verify(password, foundUser.PasswordHash);
        if (!isVerified)
        {
            throw new Exception("Failed to login");
        }

        var tokens = _jwtProvider.GenerateTokens(foundUser);
        await _refreshTokenRepository.UpdateTokenAsync(foundUser.RefreshToken?.Id, tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes() );
        return tokens;
    
    }
    
    public async Task<bool> LogOutAsync(Guid userId)
    {
        var foundUser = await _userRepository.GetByIdAsync(userId);

        if (foundUser?.RefreshToken == null)
        {
            return false;
        }
        
        return await _refreshTokenRepository.DeleteAsync(foundUser.RefreshToken.Id);
    }
}