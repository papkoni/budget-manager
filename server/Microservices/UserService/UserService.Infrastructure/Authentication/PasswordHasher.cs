using UserService.Application.Interfaces.Auth;

namespace UserService.Infrastructure;

public class PasswordHasher: IPasswordHasher
{
    public string Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password,hashPassword);
        
    }
    
}