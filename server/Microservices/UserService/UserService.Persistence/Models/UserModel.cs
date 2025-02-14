using UserService.Persistence.Enums;

namespace UserService.Persistence.Models;

public class UserModel
{
    public UserModel(Guid id, string name, string passwordHash, string email, Role role)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
        Email = email;
        Role = role;
    }
    public Guid Id { get; set; }
    public string Name { get;  set; } = string.Empty;
    public string PasswordHash { get;  set; } = string.Empty;
    public string Email { get;  set; } = string.Empty;
    public Role Role { get;  set; }
    
    public RefreshTokenModel? RefreshToken { get; set; }
}