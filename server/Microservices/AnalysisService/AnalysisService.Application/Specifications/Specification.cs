using System.Linq.Expressions;

namespace AnalysisService.Application.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public Func<T, bool> ToPredicate() => ToExpression().Compile();

    // public Specification<T> And(Specification<T> specification)
    // {
    //     return new AndSpecification<T>(this, specification);
    // }

    // public Specification<T> Or(Specification<T> specification)
    // {
    //     return new OrSpecification<T>(this, specification);
    // }
}