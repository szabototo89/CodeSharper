using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common.ControlFlow
{
    public static class ControlFlows
    {
        public static StandardControlFlow CreateStandardControlFlow()
        {
            return new StandardControlFlow();
        }
    }
}
