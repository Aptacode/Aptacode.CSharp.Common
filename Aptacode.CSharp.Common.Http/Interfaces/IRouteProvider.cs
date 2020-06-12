namespace Aptacode.CSharp.Common.Http.Interfaces
{
    /// <summary>
    ///     Creates Routes to Http resources
    /// </summary>
    public interface IRouteProvider
    {
        /// <summary>
        ///     Builds and returns the string Http Route
        /// </summary>
        /// <returns></returns>
        string Get();

        string Get(params string[] additionalSegments);


        /// <summary>
        ///     Returns a new instance of the route builder with the given segment appended
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        IRouteProvider Append(params string[] segments);

        /// <summary>
        ///     Adds the given segments to the current RouteBuilder
        /// </summary>
        /// <param name="segments"></param>
        void Join(params string[] segments);
    }
}