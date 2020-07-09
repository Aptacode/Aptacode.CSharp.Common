using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Persistence.Repository;

namespace Aptacode.CSharp.Common.Persistence.UnitOfWork
{
    public abstract class GenericUnitOfWork : IGenericUnitOfWork
    {

        #region Repositories

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public TRepo Get<TRepo, TKey, TEntity>() 
            where TEntity : IEntity<TKey>
            where TRepo : class, IGenericRepository<TKey, TEntity>
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as TRepo;
            }

            var repository = GetOrCreate<TRepo, TKey, TEntity>();
            if (repository == null)
            {
                throw new KeyNotFoundException();
            }

            Set(repository);
            return repository;
        }


        public void Set<TKey, TEntity>(IGenericRepository<TKey, TEntity> repository) 
            where TEntity : IEntity<TKey>
        {
            _repositories.Add(typeof(TEntity), repository);
        }

        protected abstract TRepo GetOrCreate<TRepo, TKey, TEntity>()
            where TEntity : IEntity<TKey>
            where TRepo : class, IGenericRepository<TKey, TEntity>;

        #endregion

        #region Unit Of Work

        public abstract void Dispose();
        public abstract Task Commit();
        public abstract void RejectChanges();

        #endregion

    }
}