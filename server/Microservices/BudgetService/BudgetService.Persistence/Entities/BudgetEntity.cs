namespace BudgetService.Persistence.Entities;

public class BudgetEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string PeriodType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Навигационное свойство для связи с категориями
    public List<BudgetCategoryEntity> BudgetCategories { get; set; }
}

