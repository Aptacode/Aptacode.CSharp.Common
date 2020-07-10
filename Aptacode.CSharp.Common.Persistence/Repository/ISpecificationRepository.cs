using System.Collections.Generic;
using Aptacode.CSharp.Common.Patterns.Specification;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface ISpecificationRepository<in TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        IReadOnlyCollection<TEntity> Get(Specification<TEntity> specification);
    }
}