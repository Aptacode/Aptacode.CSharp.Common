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

        public IGenericRepository<TKey, TEntity> Get<TKey, TEntity>() where TEntity : IEntity<TKey>
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IGenericRepository<TKey, TEntity>;
            }

            var repository = GetOrCreate<TKey, TEntity>();
            if (repository == null)
            {
                throw new KeyNotFoundException();
            }

            Set(repository);
            return repository;
        }

        public TRepo Get<TRepo, TKey, TEntity>() where TEntity : IEntity<TKey>
            where TRepo : class, IGenericRepository<TKey, TEntity>
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as TRepo;
            }

            var repository = GetOrCreate<TKey, TEntity>();
            if (repository == null)
            {
                throw new KeyNotFoundException();
            }

            Set(repository);
            return repository as TRepo;
        }


        public void Set<TKey, TEntity>(IGenericRepository<TKey, TEntity> repository) where TEntity : IEntity<TKey>
        {
            _repositories.Add(typeof(TEntity), repository);
        }

        public abstract void Dispose();
        public abstract Task Commit();
        public abstract void RejectChanges();

        protected abstract IGenericRepository<TKey, TEntity> GetOrCreate<TKey, TEntity>() where TEntity : IEntity<TKey>;
    }
}