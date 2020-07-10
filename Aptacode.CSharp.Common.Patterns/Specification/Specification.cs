using System;
using System.Linq.Expressions;

namespace Aptacode.CSharp.Common.Patterns.Specification
{
    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();

        public bool IsSatisfiedBy(T entity) => ToExpression().Compile().Invoke(entity);

        public abstract Expression<Func<T, bool>> ToExpression();

   
    }
}