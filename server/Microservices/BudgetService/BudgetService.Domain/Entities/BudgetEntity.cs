namespace BudgetService.Domain.Entities;

public class BudgetEntity
{
    public BudgetEntity(
        Guid userId,
        decimal amount,
        string currency, 
        string periodType,
        DateTime startDate,
        DateTime endDate, 
        string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Currency = currency;
        PeriodType = periodType;
        StartDate = startDate;
        EndDate = endDate;
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string PeriodType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<BudgetCategoryEntity> BudgetCategories { get; set; }
}