namespace CodeSharper.Core.Common.Interfaces
{
    /// <summary>
    /// Represents an object which has a parent
    /// </summary>
    public interface IHasParent<out TParent>
    {
        /// <summary>
        /// Gets parent of current text range
        /// </summary>
        TParent Parent { get; }
    }
}