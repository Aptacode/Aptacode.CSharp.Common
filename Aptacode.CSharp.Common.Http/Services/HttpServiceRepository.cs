using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Interfaces;
using Aptacode.CSharp.Common.Persistence;
using Aptacode.CSharp.Common.Persistence.Repository;
using AutoMapper;

namespace Aptacode.CSharp.Common.Http.Services
{
    public class HttpServiceRepository<TEntity> : HttpServiceRepository<TEntity, TEntity, TEntity>
        where TEntity : IEntity
    {
        public HttpServiceRepository(IHttpServiceClient serviceClient, IRouteProvider routeProvider, IMapper mapper) :
            base(serviceClient, routeProvider, mapper) { }
    }

    public class HttpServiceRepository<TGetViewModel, TPutViewModel, TEntity> : IRepository<TEntity>
        where TEntity : IEntity
    {
        protected readonly IMapper _mapper;
        protected readonly IRouteProvider _routeProvider;
        protected readonly IHttpServiceClient _serviceClient;

        public HttpServiceRepository(IHttpServiceClient serviceClient, IRouteProvider routeProvider, IMapper mapper)
        {
            _serviceClient = serviceClient;
            _routeProvider = routeProvider;
            _mapper = mapper;
        }

        public async Task<int> Create(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);
            var result = await _serviceClient
                .Send<TGetViewModel, TPutViewModel>(HttpMethod.Put, _routeProvider.Build(), viewmodel)
                .ConfigureAwait(false);

            if (!result.HasValue)
            {
                return -1;
            }

            var returnedEntity = _mapper.Map<TEntity>(result.Value);

            return returnedEntity.Id;
        }

        public async Task Update(TEntity entity)
        {
            var viewmodel = _mapper.Map<TPutViewModel>(entity);

            await _serviceClient
                .Send<TGetViewModel, TPutViewModel>(HttpMethod.Post, _routeProvider.Build(entity.Id), viewmodel)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await _serviceClient.Send<IEnumerable<TGetViewModel>>(HttpMethod.Get, _routeProvider.Build())
                .ConfigureAwait(false);

            if (!result.HasValue)
            {
                return null;
            }

            return result.Value.Select(r => _mapper.Map<TEntity>(r)).ToList();
        }

        public async Task<TEntity> Get(int id)
        {
            var result = await _serviceClient.Send<TGetViewModel>(HttpMethod.Get, _routeProvider.Build(id))
                .ConfigureAwait(false);
            return _mapper.Map<TEntity>(result);
        }

        public async Task Delete(int id)
        {
            await _serviceClient.Send<bool>(HttpMethod.Delete, _routeProvider.Build(id)).ConfigureAwait(false);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            var result = GetAll().Result;
            return result?.Select(r => _mapper.Map<TEntity>(r)).AsQueryable();
        }
    }
}