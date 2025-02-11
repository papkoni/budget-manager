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
    
    public async Task<TokensDTO> RefreshTokensAsync(string refreshToken)
    {
        var isValidToken = _jwtProvider.ValidateRefreshToken(refreshToken);
        if (!isValidToken)
        {
            throw new InvalidTokenException("Token was expire");
        }

        var extractedUserId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);
        var user = await _userRepository.GetByIdAsync(extractedUserId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var tokens = _jwtProvider.GenerateTokens(user);
        await _refreshTokenRepository.UpdateTokenAsync( 
            user.RefreshToken?.Id,
            tokens.RefreshToken, 
            _jwtProvider.GetRefreshTokenExpirationMinutes() );

        await _userServiceDbContext.SaveChangesAsync();
        
        return tokens;
    }
    
    public async Task<TokensDTO> RegisterAsync(RegisterUserRequest registerUser)
    {
        var validationResult =  _validator.Validate(registerUser);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            throw new BadRequestException(string.Join("; ", errors));
        }
        
        var existingUser = await _userRepository.GetByEmailAsync(registerUser.Email);
        if (existingUser != null)
        {
            throw new BadRequestException("User already exists");
        }
        
        var hashedPassword = _passwordHasher.Generate(registerUser.Password);
        
        var user = new UserModel(Guid.NewGuid(), registerUser.Password, hashedPassword, registerUser.Email, Role.User);
        
        var tokens = _jwtProvider.GenerateTokens(user);
        var refreshToken = new RefreshTokenModel(Guid.NewGuid(), tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes(), user.Id);
        
        await _userRepository.AddAsync(user);
        await _refreshTokenRepository.AddAsync(refreshToken);
        
        await _userServiceDbContext.SaveChangesAsync();

        return tokens;
    }
    
    public async Task<TokensDTO> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

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
        await _refreshTokenRepository.UpdateTokenAsync(user.RefreshToken?.Id, tokens.RefreshToken, _jwtProvider.GetRefreshTokenExpirationMinutes() );
        
        await _userServiceDbContext.SaveChangesAsync();
        
        return tokens;
    }
}