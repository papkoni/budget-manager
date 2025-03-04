namespace BudgetService.Domain.Entities;

public class CategoryEntity
{
    public CategoryEntity(
        string name, 
        decimal globalLimit,
        decimal globalSpent)
    {
        Id = Guid.NewGuid();
        Name = name;
        GlobalLimit = globalLimit;
        GlobalSpent = globalSpent;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal GlobalLimit { get; set; }
    public decimal GlobalSpent { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<BudgetCategoryEntity> BudgetCategories { get; set; }
}
