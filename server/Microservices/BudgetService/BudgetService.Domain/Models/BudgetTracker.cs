namespace BudgetService.Domain.Models;

public class BudgetTracker
{
    public Guid Id { get; set; }                   // Уникальный идентификатор записи
    public Guid BudgetId { get; set; }              // Ссылка на бюджет (budgets.id)
    public decimal CurrentSpent { get; set; }       // Текущая сумма расходов/доходов
    public decimal CurrentBalance { get; set; }     // Остаток (amount - current_spent)
    public DateTime LastUpdated { get; set; }       // Время последнего обновления

    public BudgetTracker()
    {
        Id = Guid.NewGuid();
        LastUpdated = DateTime.UtcNow;
    }
}