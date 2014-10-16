using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.ControlFlow
{
    public class StandardControlFlow
    {
        private IEnumerable<IExecutor> _executors;

        public StandardControlFlow SetControlFlow(IEnumerable<IExecutor> executors)
        {
            Constraints.NotEmpty(() => executors);
            _executors = executors;
            return this;
        }

        public Argument Execute(Argument parameter)
        {
            Argument result = parameter;

            foreach (var executor in _executors)
            {
                result = executor.Execute(result);
            }

            return result;
        }
    }
}