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
        private readonly string[] _apiBaseRoute;
        private readonly IMapper _mapper;
        protected readonly EntityMemoryCache<TEntity> MemoryCache = new EntityMemoryCache<TEntity>();

        public GenericRestRepository(IAccessTokenService authService, ServerAddress serverAddress, IMapper mapper,
            params string[] apiBaseRoute) : base(authService, serverAddress)
        {
            _mapper = mapper;
            _apiBaseRoute = apiBaseRoute;
        }

        private string[] IdRoute(int id)
        {
            var route = _apiBaseRoute.ToList();
            route.Add(id.ToString());
            return route.ToArray();
        }

        public async Task<int> Create(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);
            var result = await Put<TGetViewModel, TPutViewModel>(viewmodel, _apiBaseRoute).ConfigureAwait(false);
            var returnedEntity = _mapper.Map<TEntity>(result);
            MemoryCache.Update(returnedEntity);

            return returnedEntity.Id;
        }

        public async Task Update(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);
            var result =
                await Post<TGetViewModel, TPutViewModel>(viewmodel, IdRoute(entity.Id)).ConfigureAwait(false);
            MemoryCache.Update(_mapper.Map<TEntity>(result));
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await base.GetAll<TGetViewModel>(_apiBaseRoute).ConfigureAwait(false);

            var entities = result.Select(r => _mapper.Map<TEntity>(r));

            foreach (var entity in entities) MemoryCache.Update(entity);

            return entities;
        }

        public async Task<TEntity> Get(int id)
        {
            return await MemoryCache.GetOrCreate(id, async () =>
            {
                var result = await base.Get<TGetViewModel>(IdRoute(id))
                    .ConfigureAwait(false);
                return _mapper.Map<TEntity>(result);
            }).ConfigureAwait(false);
        }

        public async Task Delete(int id)
        {
            var result = await base.Delete(IdRoute(id)).ConfigureAwait(false);
            if (result) MemoryCache.Remove(id);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            var result = base.GetAll<TGetViewModel>(_apiBaseRoute).Result;
            return result.Select(r => _mapper.Map<TEntity>(r)).AsQueryable();
        }
    }
}