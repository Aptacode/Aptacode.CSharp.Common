using System;

namespace Aptacode.CSharp.Common.Http.Services.Extensions
{
    /// <summary>
    ///     Uri Extensions
    /// </summary>
    public static class UriExtensions
    {
        public static Uri Copy(this Uri source) => new Uri(source.ToString());
    }
}