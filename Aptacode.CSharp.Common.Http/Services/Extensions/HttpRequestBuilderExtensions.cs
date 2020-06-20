using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Aptacode.CSharp.Common.Http.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace Aptacode.CSharp.Common.Http.Services.Extensions
{
    /// <summary>
    ///     A collection of static builder extension methods for HttpRequestMessage
    /// </summary>
    public static class HttpRequestBuilderExtensions
    {
        public static HttpRequestMessage SetMethod(this HttpRequestMessage requestMessage, HttpMethod method)
        {
            requestMessage.Method = method;
            return requestMessage;
        }

        public static HttpRequestMessage SetRoute(this HttpRequestMessage requestMessage, string endPoint)
        {
            requestMessage.RequestUri = new Uri(endPoint);
            return requestMessage;
        }

        public static HttpRequestMessage AddJwtAuthentication(this HttpRequestMessage requestMessage,
            IAccessTokenService accessTokenService)
        {
            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, accessTokenService.AccessToken);
            return requestMessage;
        }

        public static HttpRequestMessage AddContent<TContent>(this HttpRequestMessage requestMessage, TContent content)
        {
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8,
                MimeTypes.MimeTypes.Application.Json.ToString());
            return requestMessage;
        }
    }
}