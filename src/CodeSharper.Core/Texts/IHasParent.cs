namespace CodeSharper.Core.Texts
{
    public interface IHasParent<out TParent>
    {
        /// <summary>
        /// Gets parent of current text range
        /// </summary>
        TParent Parent { get; }
    }
}