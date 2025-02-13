using FluentValidation;
using UserService.Application.DTO;
using UserService.Application.Exceptions;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.Persistence.Enums;
using UserService.Persistence.Interfaces;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Models;

namespace UserService.Application.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserServiceDbContext _userServiceDbContext;
    private readonly IValidator<RegisterUserRequest> _validator;

    public AuthService(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IJwtProvider jwtProvider, 
        IRefreshTokenRepository refreshTokenRepository,
        IUserServiceDbContext userServiceDbContext,
        IValidator<RegisterUserRequest> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _userServiceDbContext = userServiceDbContext;
        _validator = validator;
    }
    
    public async Task<TokensDTO> RefreshTokensAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var isValidToken = _jwtProvider.ValidateRefreshToken(refreshToken);
        if (!isValidToken)
        {
            throw new InvalidTokenException("Token was expire");
        }

        var extractedUserId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);
        var user = await _userRepository.GetByIdAsync(extractedUserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var isRevokedToken = user.RefreshToken == null || user.RefreshToken.IsRevoked;
        if (isRevokedToken)
        {
            throw new UnauthorizedException("Refresh token has been revoked.");
        }
        
        if (user.RefreshToken.ExpiryDate < DateTime.UtcNow)
        {
            throw new InvalidTokenException("Refresh token has expired.");
        }
        
        var tokens = _jwtProvider.GenerateTokens(user);
        UpdateRefreshToken(user, tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes() );
        
        await _userServiceDbContext.SaveChangesAsync(cancellationToken);
        
        return tokens;
    }
    
    public async Task<TokensDTO> RegisterAsync(RegisterUserRequest registerUser, CancellationToken cancellationToken)
    {
        var validationResult =  _validator.Validate(registerUser);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        var existingUser = await _userRepository.GetByEmailAsync(registerUser.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new BadRequestException("User already exists");
        }
        
        var hashedPassword = _passwordHasher.Generate(registerUser.Password);
        
        var user = new UserModel(Guid.NewGuid(), registerUser.Password, hashedPassword, registerUser.Email, Role.User);
        
        var tokens = _jwtProvider.GenerateTokens(user);
        var refreshToken = new RefreshTokenModel(Guid.NewGuid(), tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes(), user.Id);
        
        await _userRepository.AddAsync(user, cancellationToken);
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        
        await _userServiceDbContext.SaveChangesAsync(cancellationToken);

        return tokens;
    }
    
    public async Task<TokensDTO> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var isPasswordValid = _passwordHasher.Verify(password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new UnauthorizedException("Invalid password or email");
        }

        var tokens = _jwtProvider.GenerateTokens(user);
        UpdateRefreshToken(user, tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes() );
        
        await _userServiceDbContext.SaveChangesAsync(cancellationToken);
        
        return tokens;
    }
    
    public async Task RevokeTokenAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        user.RefreshToken.IsRevoked = true;
        
        _refreshTokenRepository.Update(user.RefreshToken);
        
        await _userServiceDbContext.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdateRefreshToken(UserModel user, string newToken, int expirationMinutes)
    {
        user.RefreshToken.Token = newToken;
        user.RefreshToken.CreatedDate = DateTime.UtcNow;
        user.RefreshToken.ExpiryDate = DateTime.UtcNow.AddMinutes(expirationMinutes);
        user.RefreshToken.IsRevoked = false;
        
        _refreshTokenRepository.Update(user.RefreshToken);
    }
}