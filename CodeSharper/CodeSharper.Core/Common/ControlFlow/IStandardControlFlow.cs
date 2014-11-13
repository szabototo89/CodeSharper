using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.ControlFlow
{
    public interface IStandardControlFlow
    {
        ICommandManager CommandManager { get; }
        Argument Execute(Argument parameter);
        StandardControlFlow ParseCommandCall(ICommandCall commandCall);
    }
}