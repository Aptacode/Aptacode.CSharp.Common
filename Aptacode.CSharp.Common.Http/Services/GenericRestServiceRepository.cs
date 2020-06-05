using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Persistence;
using Aptacode.CSharp.Common.Persistence.Cache;
using Aptacode.CSharp.Common.Persistence.Repository;
using AutoMapper;

namespace Aptacode.CSharp.Common.Http.Services
{
    public class GenericRestRepository<TGetViewModel, TPutViewModel, TEntity> : GenericHttpApiServiceClient,
        IRepository<TEntity>
        where TEntity : IEntity
    {
        private readonly IMapper _mapper;
        protected readonly EntityMemoryCache<TEntity> MemoryCache = new EntityMemoryCache<TEntity>();

        public GenericRestRepository(IAccessTokenService authService, ServerAddress serverAddress, IMapper mapper,
            params object[] apiBaseRoute) : base(authService, serverAddress, apiBaseRoute)
        {
            _mapper = mapper;
        }

        public async Task<int> Create(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);
            var result = await Put<TGetViewModel, TPutViewModel>(viewmodel).ConfigureAwait(false);
            var returnedEntity = _mapper.Map<TEntity>(result);
            MemoryCache.Update(returnedEntity);

            return returnedEntity.Id;
        }

        public async Task Update(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);
            var result =
                await Post<TGetViewModel, TPutViewModel>(viewmodel, entity.Id).ConfigureAwait(false);
            MemoryCache.Update(_mapper.Map<TEntity>(result));
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await base.GetAll<TGetViewModel>().ConfigureAwait(false);

            if (result == null)
            {
                return null;
            }

            var entities = result.Select(r => _mapper.Map<TEntity>(r)).ToList();

            entities.ToList().ForEach(MemoryCache.Update);

            return entities;
        }

        public async Task<TEntity> Get(int id)
        {
            return await MemoryCache.GetOrCreate(id, async () =>
            {
                var result = await base.Get<TGetViewModel>(id)
                    .ConfigureAwait(false);
                return _mapper.Map<TEntity>(result);
            }).ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            var result = await base.Delete(id).ConfigureAwait(false);
            if (result)
            {
                MemoryCache.Remove(id);
            }
        }

        public IQueryable<TEntity> AsQueryable()
        {
            var result = base.GetAll<TGetViewModel>().Result;

            return result?.Select(r => _mapper.Map<TEntity>(r)).AsQueryable();
        }
    }
}