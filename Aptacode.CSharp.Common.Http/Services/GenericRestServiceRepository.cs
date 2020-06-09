using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Services.Interfaces;
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
            var result = await GenericHttpMethod<TGetViewModel, TPutViewModel>(HttpMethod.Put, viewmodel)
                .ConfigureAwait(false);

            if (result.HasValue)
            {
                var returnedEntity = _mapper.Map<TEntity>(result.Value);
                MemoryCache.Update(returnedEntity);
                return returnedEntity.Id;
            }

            return -1;
        }

        public async Task Update(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);
            var result =
                await GenericHttpMethod<TGetViewModel, TPutViewModel>(HttpMethod.Post, viewmodel, entity.Id)
                    .ConfigureAwait(false);
            if (result.HasValue)
            {
                MemoryCache.Update(_mapper.Map<TEntity>(result.Value));
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await GenericHttpMethod<IEnumerable<TGetViewModel>>(HttpMethod.Get).ConfigureAwait(false);

            if (!result.HasValue)
            {
                return null;
            }

            var entities = result.Value.Select(r => _mapper.Map<TEntity>(r)).ToList();

            entities.ToList().ForEach(MemoryCache.Update);

            return entities;
        }

        public async Task<TEntity> Get(int id)
        {
            return await MemoryCache.GetOrCreate(id, async () =>
            {
                var result = await GenericHttpMethod<TGetViewModel>(HttpMethod.Get, id).ConfigureAwait(false);
                return _mapper.Map<TEntity>(result);
            }).ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            var result = await GenericHttpMethod<bool>(HttpMethod.Delete, id).ConfigureAwait(false);

            if (result.HasValue && result.Value)
            {
                MemoryCache.Remove(id);
            }
        }

        public IQueryable<TEntity> AsQueryable()
        {
            var result = GetAll().Result;
            return result?.Select(r => _mapper.Map<TEntity>(r)).AsQueryable();
        }
    }
}