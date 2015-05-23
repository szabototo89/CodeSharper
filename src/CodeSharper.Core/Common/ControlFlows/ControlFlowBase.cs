using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ControlFlowBase
    {
        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public abstract Object Execute(Object parameter);
    }
}