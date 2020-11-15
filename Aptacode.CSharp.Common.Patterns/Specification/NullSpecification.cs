using System;
using System.Linq.Expressions;

namespace Aptacode.CSharp.Common.Patterns.Specification {
    public sealed class NullSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression() => throw new NotImplementedException();
    }
}