using System.Collections.Generic;

namespace Aptacode.CSharp.Common.Persistence.Repository
{
    public interface IGenericRepository<in TKey, TEntity> where TEntity : IEntity<TKey>
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        IReadOnlyCollection<TEntity> GetAll();
        TEntity Get(TKey id);
        void Delete(TKey id);
    }
}