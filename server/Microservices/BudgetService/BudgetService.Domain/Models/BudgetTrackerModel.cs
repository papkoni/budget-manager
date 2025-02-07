namespace BudgetService.Domain.Models;

public class BudgetTrackerModel
{
    public BudgetTrackerModel(Guid budgetId, decimal currentSpent, decimal currentBalance)
    {
        Id = Guid.NewGuid();
        BudgetId = budgetId;
        CurrentSpent = currentSpent;
        CurrentBalance = currentBalance;
        LastUpdated = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }         // Уникальный идентификатор записи
    public Guid BudgetId { get; set; }          // Ссылка на бюджет
    public decimal CurrentSpent { get; set; }   // Текущая сумма расходов/доходов
    public decimal CurrentBalance { get; set; } // Остаток
    public DateTime LastUpdated { get; private set; } // Время последнего обновления
}
