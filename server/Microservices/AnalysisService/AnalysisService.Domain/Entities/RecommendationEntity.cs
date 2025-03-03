namespace AnalysisService.Domain.Entities;

public class RecommendationEntity
{
    public Guid Id { get; set; }  // Идентификатор рекомендации
    
    public string Category { get; set; }  // Категория расходов
    
    public string Recommendation { get; set; }  // Текст рекомендации
    
    public DateTime CreatedAt { get; set; }  // Дата создания рекомендации
}