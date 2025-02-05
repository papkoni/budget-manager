using System.ComponentModel;

namespace UserService.Persistence.Enums;

public enum Role
{
    [Description("User")]
    User = 0,
    [Description("Admin")]
    Admin = 1
}