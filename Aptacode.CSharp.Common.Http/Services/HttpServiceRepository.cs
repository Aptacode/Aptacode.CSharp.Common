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
    public class HttpServiceRepository<TKey, TEntity> : HttpServiceRepository<TKey, TEntity, TEntity, TEntity>
        where TEntity : IEntity<TKey>
    {
        public HttpServiceRepository(IHttpServiceClient serviceClient, IRouteProvider routeProvider, IMapper mapper) :
            base(serviceClient, routeProvider, mapper) { }
    }

    public class HttpServiceRepository<TKey, TGetViewModel, TPutViewModel, TEntity> : IGenericAsyncRepository<TKey, TEntity>
        where TEntity : IEntity<TKey>
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

        public async Task CreateAsync(TEntity entity)
        {
            var viewmodel = Mapper.Map<TPutViewModel>(entity);
            var result = await ServiceClient
                .Send<TGetViewModel, TPutViewModel>(HttpMethod.Put, RouteProvider.Get(), viewmodel)
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var viewmodel = Mapper.Map<TPutViewModel>(entity);

            await ServiceClient
                .Send<TGetViewModel, TPutViewModel>(HttpMethod.Post, RouteProvider.Get(entity.Id.ToString()),
                    viewmodel)
                .ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            var result = await ServiceClient.Send<IEnumerable<TGetViewModel>>(HttpMethod.Get, RouteProvider.Get())
                .ConfigureAwait(false);

            return !result.HasValue ? null : result.Value.Select(r => Mapper.Map<TEntity>(r)).ToList();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            var result = await ServiceClient.Send<TGetViewModel>(HttpMethod.Get, RouteProvider.Get(id.ToString()))
                .ConfigureAwait(false);
            return Mapper.Map<TEntity>(result);
        }

        public async Task DeleteAsync(TKey id)
        {
            await ServiceClient.Send<bool>(HttpMethod.Delete, RouteProvider.Get(id.ToString())).ConfigureAwait(false);
        }
    }
}