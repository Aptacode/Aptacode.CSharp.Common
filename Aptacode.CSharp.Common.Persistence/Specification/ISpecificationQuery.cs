using System.Collections.Generic;

namespace Aptacode.CSharp.Common.Persistence.Specification
{
    public interface ISpecificationQuery<in TKey, TEntity> where TEntity : IEntity<TKey>
    {
        IReadOnlyCollection<TEntity> Get(Specification<TEntity> specification);
    }
}