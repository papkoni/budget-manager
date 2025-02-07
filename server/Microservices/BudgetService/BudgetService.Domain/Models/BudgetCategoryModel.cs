namespace BudgetService.Domain.Models;

public class BudgetCategoryModel
{
    public BudgetCategoryModel(Guid budgetId, Guid categoryId, decimal amount, decimal spent)
    {
        Id = Guid.NewGuid();
        BudgetId = budgetId;
        CategoryId = categoryId;
        Amount = amount;
        Spent = spent;
    }
    public Guid Id { get; private set; }
    public Guid BudgetId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }  
    public decimal Spent { get; set; }   // Потрачено в рамках бюджета
}