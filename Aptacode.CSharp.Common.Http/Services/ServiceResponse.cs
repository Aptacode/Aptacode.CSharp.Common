using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aptacode.CSharp.Common.Http.Services
{
    public class ServiceResponse<T>
    {
        protected ServiceResponse()
        {
            Content = default;
            Status = string.Empty;
            HasValue = false;
        }

        protected ServiceResponse(string status)
        {
            Content = default;
            Status = status;
            HasValue = false;
        }

        protected ServiceResponse(T content, string status)
        {
            Content = content;
            Status = status;
            HasValue = true;
        }

        public T Content { get; protected set; }
        public string Status { get; protected set; }

        public bool HasValue { get; protected set; }

        #region Factory Methods

        public static ServiceResponse<T> Create(T content, string status) => new ServiceResponse<T>(content, status);

        public static ServiceResponse<T> Create(string status) => new ServiceResponse<T>(status);

        public static ServiceResponse<T> Create() => new ServiceResponse<T>();

        public static async Task<ServiceResponse<T>> Create(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var content = JsonConvert.DeserializeObject<T>(body);
                    return Create(content, response.ReasonPhrase);
                }
                catch
                {
                    new ServiceResponse<T>("Error decoding message");
                }
            }

            return Create(response.ReasonPhrase);
        }

        #endregion
    }
}