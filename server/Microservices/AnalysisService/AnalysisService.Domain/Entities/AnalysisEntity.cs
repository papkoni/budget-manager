namespace AnalysisService.Domain.Entities;

public class AnalysisEntity
{
    public Guid Id { get; set; }  // Уникальный идентификатор аналитической записи
    
    public Guid Category { get; set; }  // Категория расходов
    
    public decimal TotalAmount { get; set; }  // Общая сумма расходов по категории
    
    public decimal AverageAmount { get; set; }  // Средняя сумма расходов по категории
    
    public DateTime PeriodStart { get; set; }  // Начало периода
    
    public DateTime PeriodEnd { get; set; }  // Конец периода
    
    public DateTime CreatedAt { get; set; }  // Дата создания записи
    
    public DateTime UpdatedAt { get; set; }  // Дата последнего обновления записи
}