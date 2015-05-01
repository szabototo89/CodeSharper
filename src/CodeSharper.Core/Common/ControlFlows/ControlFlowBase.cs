using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ControlFlowBase
    {
        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public abstract Object Execute(Object parameter);
    }

    public class SelectorControlFlowBase : ControlFlowBase
    {
        /// <summary>
        /// Executes the specified parameter
        /// </summary>
        public override Object Execute(Object parameter)
        {
            throw new NotImplementedException();
        }
    }
}