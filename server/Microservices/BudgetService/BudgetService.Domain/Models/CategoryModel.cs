namespace BudgetService.Domain.Models;

public class CategoryModel
{
    public CategoryModel(string name, string type, decimal globalLimit, decimal globalSpent)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        GlobalLimit = globalLimit;
        GlobalSpent = globalSpent;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Type { get; set; } // "income" или "expense"
    public decimal GlobalLimit { get; set; }
    public decimal GlobalSpent { get; set; }
    public DateTime CreatedAt { get; private set; }
}