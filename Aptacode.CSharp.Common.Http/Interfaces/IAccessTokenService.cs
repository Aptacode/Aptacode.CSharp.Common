using System;

namespace Aptacode.CSharp.Common.Http.Interfaces
{
    /// <summary>
    ///     Generates an AccessToken for Http Requests
    /// </summary>
    public interface IAccessTokenService
    {
        string AccessToken { get; }
        bool HasValidAccessToken { get; }

        event EventHandler<string> OnTokenChanged;
    }
}