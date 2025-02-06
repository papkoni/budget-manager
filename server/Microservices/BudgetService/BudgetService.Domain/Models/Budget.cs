namespace BudgetService.Domain.Models;

public class Budget
{
    public Budget()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }                   // Уникальный идентификатор бюджета
    public Guid UserId { get; set; }               // Ссылка на пользователя
    public Guid CategoryId { get; set; }           // Категория транзакции
    public decimal Amount { get; set; }            // Лимит бюджета
    public string Currency { get; set; }           // Валюта (USD, EUR и т.д.)
    public string PeriodType { get; set; }         // Тип периода (месяц, квартал, год, custom)
    public DateTime StartDate { get; set; }        // Начало периода
    public DateTime? EndDate { get; set; }         // Конец периода (для period_type=custom)
    public string Status { get; set; }             // Активен/Неактивен/Приостановлен
    public DateTime CreatedAt { get; set; }        // Дата создания
    public DateTime UpdatedAt { get; set; }        // Дата последнего обновления
}