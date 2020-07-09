using System;
using System.Linq.Expressions;

namespace Aptacode.CSharp.Common.Persistence.Specification.Composition
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
}