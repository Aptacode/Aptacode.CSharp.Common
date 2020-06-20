namespace Aptacode.CSharp.Common.Persistence.Cache
{
    public class EntityMemoryCache<TKey, TEntity> : GenericMemoryCache<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        public void Update(TEntity entity)
        {
            Update(entity.Id, entity);
        }

        public void Remove(TEntity entity)
        {
            Remove(entity.Id);
        }
    }
}