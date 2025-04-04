using System.ComponentModel;

namespace BudgetService.Domain.Enums;

public enum Period
{
    [Description("Monthly")]
    Monthly = 0,
    [Description("Annually")]
    Annually = 1
}