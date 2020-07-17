namespace Aptacode.CSharp.Common.Patterns.Visitor {
    public interface IVisited<TElement, in TVisitor> where TVisitor : IVisitor<TElement>
    {
        public void Accept(TVisitor visitor);
    }
}