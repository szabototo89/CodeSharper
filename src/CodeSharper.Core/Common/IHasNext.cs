namespace CodeSharper.Core.Common
{
    public interface IHasNext<out TValue>
    {
        /// <summary>
        /// Gets the next element of object
        /// </summary>
        TValue Next { get; }
    }
}