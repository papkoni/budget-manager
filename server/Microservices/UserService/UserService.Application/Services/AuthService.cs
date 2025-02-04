using UserService.Application.DTO;
using UserService.Application.Enums;
using UserService.Application.Exceptions;
using UserService.Application.Interfaces;
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
    private readonly IUserServiceDbContext _userServiceDbContext;

    public AuthService(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IJwtProvider jwtProvider, 
        IRefreshTokenRepository refreshTokenRepository,
        IUserServiceDbContext userServiceDbContext)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _userServiceDbContext = userServiceDbContext;
    }
    
    
    public async Task<TokensDTO> RefreshTokensAsync(string refreshToken)
    {
        var isValidToken = _jwtProvider.ValidateRefreshToken(refreshToken);
        if (!isValidToken)
        {
            throw new InvalidTokenException("Token was expire");
        }

        var extractedUserId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);
        var foundUser = await _userRepository.GetByIdAsync(extractedUserId);
        if (foundUser == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var tokens = _jwtProvider.GenerateTokens(foundUser);
        await _refreshTokenRepository.UpdateTokenAsync( 
            foundUser.RefreshToken?.Id,
            tokens.RefreshToken, 
            _jwtProvider.GetRefreshTokenExpirationMinutes() );

        await _userServiceDbContext.SaveChangesAsync();
        
        return tokens;
    }
    
    public async Task<TokensDTO> RegisterAsync(string name, string password, string email)
    {
    
        var foundUser = await _userRepository.GetByEmailAsync(email);
        if (foundUser != null)
        {
            throw new BadRequestException("User already exists");
        }
    
        var hashedPassword = _passwordHasher.Generate(password);

        var user = new UserModel(Guid.NewGuid(), name, hashedPassword, email, Role.User);
        
        var tokens = _jwtProvider.GenerateTokens(user);
        var refreshToken = new RefreshTokenModel(Guid.NewGuid(), tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes(), user.Id);
        
        await _userRepository.AddAsync(user);
        await _refreshTokenRepository.AddAsync(refreshToken);
        
        await _userServiceDbContext.SaveChangesAsync();

        return tokens;
    }
    
    
    
    public async Task<TokensDTO> LoginAsync(string email, string password)
    {
        var foundUser = await _userRepository.GetByEmailAsync(email);

        if (foundUser == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var isVerified = _passwordHasher.Verify(password, foundUser.PasswordHash);
        if (!isVerified)
        {
            throw new BadRequestException("Check password or email");
        }

        var tokens = _jwtProvider.GenerateTokens(foundUser);
        await _refreshTokenRepository.UpdateTokenAsync(foundUser.RefreshToken?.Id, tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes() );
        
        await _userServiceDbContext.SaveChangesAsync();
        
        return tokens;
    }
    
    public async Task<bool> LogOutAsync(Guid userId)
    {
        var foundUser = await _userRepository.GetByIdAsync(userId);
        if (foundUser?.RefreshToken == null)
        {
            throw new NotFoundException("User not found");
        }

        await _refreshTokenRepository.DeleteAsync(foundUser.RefreshToken.Id);
        
        await _userServiceDbContext.SaveChangesAsync();
        
        return true;
        
        
    }
}