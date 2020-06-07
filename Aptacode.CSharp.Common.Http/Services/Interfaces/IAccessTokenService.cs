using System;

namespace Aptacode.CSharp.Common.Http.Services.Interfaces
{
    public interface IAccessTokenService
    {
        string AccessToken { get; }
        bool HasValidAccessToken { get; }

        event EventHandler<string> OnTokenChanged;
    }
}