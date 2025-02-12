namespace BudgetService.Domain.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; } // "income" или "expense"
    public decimal GlobalLimit { get; set; }
    public decimal GlobalSpent { get; set; }
    public DateTime CreatedAt { get; set; }

    // Навигационное свойство для связи многие ко многим
    public List<BudgetCategoryEntity> BudgetCategories { get; set; }
}
