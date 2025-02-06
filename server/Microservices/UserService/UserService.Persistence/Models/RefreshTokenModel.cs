namespace UserService.Persistence.Models;

public class RefreshTokenModel
{
    public RefreshTokenModel() { }

    public RefreshTokenModel(Guid id, string token, int expiryDate, Guid? userId )
    {
        Id = id;
        Token = token;
        CreatedDate = DateTime.Now ;
        ExpiryDate = CreatedDate.AddMinutes(expiryDate);
        UserId = userId;
    }
    
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ExpiryDate { get; set; } 
    public virtual UserModel User { get; set; } = null!;
}