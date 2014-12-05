using CodeSharper.Core.Commands;

namespace CodeSharper.Core.Common.ControlFlow
{
    public interface IControlFlowFactory
    {
        IControlFlow CreateControlFlow(ICommandCall commandCall);
    }
}