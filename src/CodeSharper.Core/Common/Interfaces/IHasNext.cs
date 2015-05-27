namespace CodeSharper.Core.Common.Interfaces
{
    public interface IHasNext<out TValue>
    {
        /// <summary>
        /// Gets the next element of Object
        /// </summary>
        TValue Next { get; }
    }
}