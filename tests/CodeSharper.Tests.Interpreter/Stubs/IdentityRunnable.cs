using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Tests.Interpreter.Stubs
{
    public class IdentityRunnable : IRunnable
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public Object Run(Object parameter)
        {
            return (Int32)parameter + 1;
        }
    }
}
