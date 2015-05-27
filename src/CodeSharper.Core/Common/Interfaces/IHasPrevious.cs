namespace CodeSharper.Core.Common.Interfaces
{
    public interface IHasPrevious<out TValue>
    {
        /// <summary>
        /// Gets the previous element of Object
        /// </summary>
        TValue Previous { get; }
    }
}