using CodeSharper.Core.Common.Interfaces;

namespace CodeSharper.Core.Common
{
    public interface ILinkedStyle<out TValue> : IHasNext<TValue>, IHasPrevious<TValue> { }
}