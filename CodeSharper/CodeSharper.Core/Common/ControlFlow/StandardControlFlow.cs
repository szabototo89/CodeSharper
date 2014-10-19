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
        private readonly List<IExecutor> _executors;

        public StandardControlFlow()
        {
            _executors = new List<IExecutor>();
        }

        public StandardControlFlow Clear()
        {
            _executors.Clear();
            return this;
        }

        public StandardControlFlow AddRunnable<TIn, TOut>(IRunnable<TIn, TOut> runnable)
        {
            _executors.Add(Executors.CreateStandardExecutor(runnable));
            return this;
        }

        public StandardControlFlow SetControlFlow(IEnumerable<IExecutor> executors)
        {
            Constraints.NotEmpty(() => executors);
            Clear();
            _executors.AddRange(executors);
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