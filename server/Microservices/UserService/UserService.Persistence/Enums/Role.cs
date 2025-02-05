using System.ComponentModel;

namespace UserService.Application.Enums;

public enum Role
{
    [Description("User")]
    User = 0,
    [Description("Admin")]
    Admin = 1
}