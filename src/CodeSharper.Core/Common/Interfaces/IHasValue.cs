namespace CodeSharper.Core.Common.Interfaces
{
    public interface IHasValue<out TValue>
    {
        TValue Value { get; }
    }
}