using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;

namespace CodeSharper.Core.Common.ControlFlow
{
    public static class ControlFlows
    {
        public static StandardControlFlow CreateStandardControlFlow(ICommandManager manager, IExecutor executor)
        {
            return new StandardControlFlow(manager, executor);
        }
    }
}
