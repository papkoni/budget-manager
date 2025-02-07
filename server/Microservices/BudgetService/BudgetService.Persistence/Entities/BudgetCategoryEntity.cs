namespace BudgetService.Persistence.Entities;

public class BudgetCategoryEntity
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }  // Лимит внутри бюджета
    public decimal Spent { get; set; }   // Потрачено в рамках бюджета

    // Навигационные свойства
    public BudgetEntity Budget { get; set; }
    public CategoryEntity Category { get; set; }
}
