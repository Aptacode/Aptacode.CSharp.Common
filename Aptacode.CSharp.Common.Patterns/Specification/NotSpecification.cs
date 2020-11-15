using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aptacode.CSharp.Common.Patterns.Specification
{
    public sealed class NotSpecification<T> : Specification<T>
    {
        public Specification<T> Specification { get; set; }

        public NotSpecification(Specification<T> specification)
        {
            this.Specification = specification;
        }

        public NotSpecification() : this(new NullSpecification<T>())
        {

        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = Specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}