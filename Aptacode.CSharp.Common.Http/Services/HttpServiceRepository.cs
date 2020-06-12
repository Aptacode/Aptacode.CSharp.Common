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
        protected readonly IMapper Mapper;
        protected readonly IRouteProvider RouteProvider;
        protected readonly IHttpServiceClient ServiceClient;

        public HttpServiceRepository(IHttpServiceClient serviceClient, IRouteProvider routeProvider, IMapper mapper)
        {
            ServiceClient = serviceClient;
            RouteProvider = routeProvider;
            Mapper = mapper;
        }

        public async Task<int> Create(TEntity entity)
        {
            var viewmodel = Mapper.Map<TPutViewModel>(entity);
            var result = await ServiceClient
                .Send<TGetViewModel, TPutViewModel>(HttpMethod.Put, RouteProvider.Build(), viewmodel)
                .ConfigureAwait(false);

            if (!result.HasValue)
            {
                return -1;
            }

            var returnedEntity = Mapper.Map<TEntity>(result.Value);

            return returnedEntity.Id;
        }

        public async Task Update(TEntity entity)
        {
            var viewmodel = Mapper.Map<TPutViewModel>(entity);

            await ServiceClient
                .Send<TGetViewModel, TPutViewModel>(HttpMethod.Post, RouteProvider.Build(entity.Id), viewmodel)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await ServiceClient.Send<IEnumerable<TGetViewModel>>(HttpMethod.Get, RouteProvider.Build())
                .ConfigureAwait(false);

            return !result.HasValue ? null : result.Value.Select(r => Mapper.Map<TEntity>(r)).ToList();
        }

        public async Task<TEntity> Get(int id)
        {
            var result = await ServiceClient.Send<TGetViewModel>(HttpMethod.Get, RouteProvider.Build(id))
                .ConfigureAwait(false);
            return Mapper.Map<TEntity>(result);
        }

        public async Task Delete(int id)
        {
            await ServiceClient.Send<bool>(HttpMethod.Delete, RouteProvider.Build(id)).ConfigureAwait(false);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            var result = GetAll().Result;
            return result?.Select(r => Mapper.Map<TEntity>(r)).AsQueryable();
        }
    }
}