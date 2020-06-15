using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aptacode.CSharp.Common.Http.Services.Responses
{
    /// <summary>
    ///     Represents a response from a REST Service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpServiceResponse<T> : ServiceResponse<T>
    {
        protected HttpServiceResponse(HttpResponseMessage responseMessage, string message) : base(message)
        {
            HttpResponse = responseMessage;
        }

        protected HttpServiceResponse(string message) : base(message)
        {
            HttpResponse = null;
        }

        protected HttpServiceResponse(T value, string message) : base(value, message)
        {
            HttpResponse = null;
        }

        protected HttpServiceResponse(T value, HttpResponseMessage responseMessage) : base(value,
            responseMessage.ReasonPhrase)
        {
            HttpResponse = responseMessage;
        }

        public HttpResponseMessage HttpResponse { get; protected set; }

        #region Factory Methods

        public new static HttpServiceResponse<T> Create(string message) => new HttpServiceResponse<T>(message);

        public new static HttpServiceResponse<T> Create(T value, string message) =>
            new HttpServiceResponse<T>(value, message);

        public static HttpServiceResponse<T> Create(T value, HttpResponseMessage message) =>
            new HttpServiceResponse<T>(value, message);

        public static async Task<HttpServiceResponse<T>> Create(HttpResponseMessage responseMessage) =>
            await Create(responseMessage, responseMessage.ReasonPhrase).ConfigureAwait(false);

        public static async Task<HttpServiceResponse<T>> Create(HttpResponseMessage responseMessage, string message)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new HttpServiceResponse<T>(responseMessage, message);
            }

            try
            {
                var body = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var value = JsonConvert.DeserializeObject<T>(body);
                return new HttpServiceResponse<T>(value, responseMessage);
            }
            catch
            {
                return new HttpServiceResponse<T>(responseMessage, "Error decoding value");
            }
        }

        #endregion
    }
}