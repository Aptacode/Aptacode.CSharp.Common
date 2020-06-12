using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Interfaces;
using Aptacode.CSharp.Common.Http.Services.Responses;
using AutoMapper;

namespace Aptacode.CSharp.Common.Http.Services
{
    public class HttpRestService<T> : HttpRestService<T, T>
    {
        public HttpRestService(IHttpServiceClient serviceClient, IRouteProvider routeProvider, IMapper mapper) :
            base(serviceClient, routeProvider, mapper) { }
    }

    public class HttpRestService<TResponse, TRequest>
    {
        protected readonly IMapper Mapper;
        protected readonly IRouteProvider RouteProvider;
        protected readonly IHttpServiceClient ServiceClient;

        public HttpRestService(IHttpServiceClient serviceClient, IRouteProvider routeProvider, IMapper mapper)
        {
            ServiceClient = serviceClient;
            RouteProvider = routeProvider;
            Mapper = mapper;
        }

        public async Task<HttpServiceResponse<IEnumerable<TResponse>>> GetAll() =>
            await ServiceClient.Send<IEnumerable<TResponse>>(HttpMethod.Get, RouteProvider.Get())
                .ConfigureAwait(false);

        public async Task<HttpServiceResponse<TResponse>> Get(int id) =>
            await ServiceClient.Send<TResponse>(HttpMethod.Get, RouteProvider.Get(id.ToString()))
                .ConfigureAwait(false);

        public async Task<HttpServiceResponse<TResponse>> Update(int id, TRequest content) =>
            await ServiceClient.Send<TResponse, TRequest>(HttpMethod.Post, RouteProvider.Get(id.ToString()), content)
                .ConfigureAwait(false);

        public async Task<HttpServiceResponse<TResponse>> Create(TRequest content) =>
            await ServiceClient.Send<TResponse, TRequest>(HttpMethod.Put, RouteProvider.Get(), content)
                .ConfigureAwait(false);

        public async Task<HttpServiceResponse<bool>> Delete(int id) =>
            await ServiceClient.Send<bool>(HttpMethod.Delete, RouteProvider.Get(id.ToString())).ConfigureAwait(false);
    }
}