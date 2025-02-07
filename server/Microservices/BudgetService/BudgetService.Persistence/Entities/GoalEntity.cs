namespace BudgetService.Persistence.Entities;

public class GoalEntity
{
    public Guid Id { get; set; }                   // Уникальный идентификатор цели
    public Guid UserId { get; set; }                // Ссылка на пользователя
    public decimal TargetAmount { get; set; }       // Целевая сумма
    public decimal CurrentAmount { get; set; }      // Текущий прогресс
    public DateTime Deadline { get; set; }          // Дата завершения цели
    public DateTime CreatedAt { get; set; }         // Дата создания
}