namespace BudgetService.Domain.Entities;

public class GoalEntity
{
    public GoalEntity(
        Guid userId,
        decimal targetAmount,
        decimal currentAmount, 
        DateTime deadline,
        string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TargetAmount = targetAmount;
        CurrentAmount = currentAmount;
        Deadline = deadline;
        CreatedAt = DateTime.UtcNow;
        Name = name;
    }
    public Guid Id { get; set; }                   // Уникальный идентификатор цели
    public Guid UserId { get; set; }                // Ссылка на пользователя
    public decimal TargetAmount { get; set; }       // Целевая сумма
    public string Name { get; set; } 
    public decimal CurrentAmount { get; set; }      // Текущий прогресс
    public DateTime Deadline { get; set; }          // Дата завершения цели
    public DateTime CreatedAt { get; set; }         // Дата создания
}