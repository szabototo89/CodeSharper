namespace CodeSharper.Core.Common
{
    public interface IHasValue<out TValue>
    {
        TValue Value { get; }
    }
}