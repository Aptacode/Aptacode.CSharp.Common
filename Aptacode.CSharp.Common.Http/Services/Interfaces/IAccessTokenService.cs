using System;

namespace Aptacode.CSharp.Common.Http.Services.Interfaces
{
    public interface IAccessTokenService
    {
        string GetAccessToken();
        event EventHandler<string> OnTokenChanged;
    }
}