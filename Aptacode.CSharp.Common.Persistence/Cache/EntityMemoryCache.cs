namespace Aptacode.CSharp.Common.Persistence.Cache
{
    public class EntityMemoryCache<TEntity> : GenericMemoryCache<int, TEntity> where TEntity : IEntity
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