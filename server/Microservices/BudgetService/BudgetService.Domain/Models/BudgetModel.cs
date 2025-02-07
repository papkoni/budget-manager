namespace BudgetService.Domain.Models;

public class BudgetModel
{
    public BudgetModel(Guid userId, decimal amount, string currency, string periodType, DateTime startDate, DateTime? endDate, string status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Currency = currency;
        PeriodType = periodType;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string PeriodType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
