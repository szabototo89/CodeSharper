namespace CodeSharper.Core.Common
{
    public interface IHasPrevious<out TValue>
    {
        /// <summary>
        /// Gets the previous element of object
        /// </summary>
        TValue Previous { get; }
    }
}