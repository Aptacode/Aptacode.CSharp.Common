namespace Aptacode.CSharp.Common.Http.Interfaces
{
    public interface IRouteProvider
    {
        string Build(params object[] additionalSegments);
    }
}