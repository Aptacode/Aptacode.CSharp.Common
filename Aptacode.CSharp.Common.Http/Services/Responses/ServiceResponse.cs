namespace Aptacode.CSharp.Common.Http.Services.Responses
{
    /// <summary>
    ///     Represents a generic response from a service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        protected ServiceResponse()
        {
            Value = default;
            Message = string.Empty;
            HasValue = false;
        }

        protected ServiceResponse(string message)
        {
            Value = default;
            Message = message;
            HasValue = false;
        }

        protected ServiceResponse(T value, string message)
        {
            Value = value;
            Message = message;
            HasValue = true;
        }

        public T Value { get; protected set; }
        public string Message { get; protected set; }

        public bool HasValue { get; protected set; }

        #region Factory Methods

        public static ServiceResponse<T> Create(T content, string status) => new ServiceResponse<T>(content, status);

        public static ServiceResponse<T> Create(string status) => new ServiceResponse<T>(status);

        public static ServiceResponse<T> Create() => new ServiceResponse<T>();

        #endregion
    }
}