namespace BudgetService.Domain.Entities;

public class BudgetCategoryEntity
{
    public BudgetCategoryEntity(Guid budgetId, 
        Guid categoryId, 
        decimal amount, 
        decimal spent)
    {
        Id = Guid.NewGuid();
        BudgetId = budgetId;
        CategoryId = categoryId;
        Amount = amount;
        Spent = spent;
    }
    
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; } 
    public decimal Spent { get; set; } 

    public BudgetEntity Budget { get; set; }
    public CategoryEntity Category { get; set; }
}
