namespace CodeSharper.Core.Common
{
    public interface IHasParent<out TParent>
    {
        /// <summary>
        /// Gets parent of current text range
        /// </summary>
        TParent Parent { get; }
    }
}