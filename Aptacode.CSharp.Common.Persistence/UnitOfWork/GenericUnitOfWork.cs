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

        public IRepository<TEntity> Repository<TEntity>() where TEntity : IEntity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var repo = CreateRepository<TEntity>();
            if (repo != null)
            {
                SetRepository(repo);
            }

            return repo;
        }

        public void SetRepository<TEntity>(IRepository<TEntity> repository) where TEntity : IEntity
        {
            _repositories.Add(typeof(TEntity), repository);
        }

        public abstract void Dispose();
        public abstract Task Commit();
        public abstract void RejectChanges();

        protected abstract IRepository<TEntity> CreateRepository<TEntity>() where TEntity : IEntity;
    }
}