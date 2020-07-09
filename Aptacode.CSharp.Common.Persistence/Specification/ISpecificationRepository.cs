using System.Collections.Generic;
using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.Specification
{
    public interface ISpecificationRepository<in TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        IReadOnlyCollection<TEntity> Get(Specification<TEntity> specification);
    }
}