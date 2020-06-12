using System.Net.Http;
using System.Threading.Tasks;
using Aptacode.CSharp.Common.Http.Services.Responses;

namespace Aptacode.CSharp.Common.Http.Interfaces
{
    /// <summary>
    ///     Interface for sending & receiving C# objects to / from a restful service
    /// </summary>
    public interface IHttpServiceClient
    {
        Task<HttpServiceResponse<TReturn>> Send<TReturn, TSend>(HttpMethod method, string route, TSend content);
        Task<HttpServiceResponse<TReturn>> Send<TReturn>(HttpMethod method, string route);
    }
}