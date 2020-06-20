using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public abstract class GenericUnitOfWork : IGenericUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public IRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : IEntity<TKey>
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TKey, TEntity>;
            }

            var repo = CreateRepository<TKey, TEntity>();
            if (repo != null)
            {
                SetRepository(repo);
            }

            return repo;
        }

        public void SetRepository<TKey, TEntity>(IRepository<TKey, TEntity> repository) where TEntity : IEntity<TKey>
        {
            _repositories.Add(typeof(TEntity), repository);
        }

        public abstract void Dispose();
        public abstract Task Commit();
        public abstract void RejectChanges();

        protected abstract IRepository<TKey, TEntity> CreateRepository<TKey, TEntity>() where TEntity : IEntity<TKey>;
    }
}