using System;
using System.Linq.Expressions;

namespace Aptacode.CSharp.Common.Patterns.Specification
{
    public sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return _ => true;
        }
    }
}