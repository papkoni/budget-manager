namespace UserService.Application.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    
    public UserModel(Guid id, string name, string passwordHash, string email, string role)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
        Email = email;
        Role = role;
    }
}