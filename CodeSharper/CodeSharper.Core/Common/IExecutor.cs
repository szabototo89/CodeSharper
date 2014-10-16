using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public interface IExecutor
    {
        Argument Execute(Argument parameter);
    }
}