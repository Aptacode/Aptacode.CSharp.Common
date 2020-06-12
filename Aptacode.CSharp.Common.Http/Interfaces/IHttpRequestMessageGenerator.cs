using System.Net.Http;

namespace Aptacode.CSharp.Common.Http.Interfaces
{
    /// <summary>
    ///     Generates HttpRequestMessages from C# objects
    /// </summary>
    public interface IHttpRequestGenerator
    {
        HttpRequestMessage CreateRequest(HttpMethod method, string route);
        HttpRequestMessage CreateRequest<TContent>(HttpMethod method, string route, TContent content);
    }
}