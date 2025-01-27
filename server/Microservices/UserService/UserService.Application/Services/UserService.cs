using UserService.Application.Interfaces.Auth;
using UserService.Application.Models;
using UserService.Application.Interfaces.Repositories;
namespace UserService.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    private readonly IPasswordHasher _passwordHasher;
    
    // public async Task<(string, string)> Register(string name, string password, string email)
    // {
    //
    //     var findUser = await _userRepository.GetByEmail(email);
    //     if (findUser != null)
    //     {
    //         throw new Exception("User already exists");
    //     }
    //
    //     var hashedPassword = _passwordHasher.Generate(password);
    //         
    //
    //     var user = new UserModel(Guid.NewGuid(), name, hashedPassword, email);
    //     var (accessToken, refreshToken) = _jwtProvider.Generate(user);
    //     var token = RefreshToken.Create(Guid.NewGuid(), refreshToken, DateTime.Now);
    //     user.RefreshTokenId = token.Id;
    //     await _refreshTokensRepository.Add(token);
    //     await _usersRepository.Add(user);
    //
    //     return (accessToken, refreshToken );
    // }
    
    
    
    // public async Task<(string, string, UserModel?)> Login(string email, string password)
    // {
    //     var findUser = await _userRepository.GetByEmail(email);
    //
    //     if (findUser == null) { throw new Exception(); }
    //
    //
    //     var result = _passwordHasher.Verify(password, findUser.PasswordHash);
    //
    //     if (result == false)
    //     {
    //         throw new Exception("Failed to login");
    //     }
    //
    //     var (accessToken, refreshToken) = _jwtProvider.Generate(findUser);
    //     await _refreshTokensRepository.UpdateToken(findUser.RefreshTokenId, refreshToken);
    //     return (accessToken, refreshToken, findUser);
    //
    // }
}