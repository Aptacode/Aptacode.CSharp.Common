namespace Aptacode.CSharp.Common.Persistence
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}