namespace AnalysisService.Domain.Entities;

using System;

public class TransactionEntity
{
    public Guid Id { get; set; }  // Идентификатор транзакции (генерируется автоматически)
    
    public Guid UserId { get; set; }  // Идентификатор пользователя
    
    public string Category { get; set; }  // Категория расходов
    
    public decimal Amount { get; set; }  // Сумма транзакции
    
    public string Description { get; set; }  // Описание
    
    public DateTime Date { get; set; }  // Дата транзакции
    
    public DateTime CreatedAt { get; set; }  // Дата создания записи
    
    public DateTime UpdatedAt { get; set; }  // Дата последнего обновления записи
}
