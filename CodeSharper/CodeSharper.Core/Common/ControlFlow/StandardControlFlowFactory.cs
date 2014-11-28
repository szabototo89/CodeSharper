using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common.ControlFlow
{
    public class StandardControlFlowFactory
    {
        public IControlFlow Parse(ICommandCall commandCall)
        {
            Constraints.NotNull(() => commandCall);

            return null;
        }
    }
}