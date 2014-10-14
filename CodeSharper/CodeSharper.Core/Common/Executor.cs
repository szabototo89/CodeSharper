using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common
{
    public class Executor
    {
        private readonly IRunnable _runnable;

        public Executor(IRunnable runnable)
        {
            _runnable = runnable;
        }

        public Argument Execute(Argument parameter)
        {
            var converter = _runnable.SupportedArgumentWrappers.FirstOrDefault(c => c.IsConvertable(parameter));

            if (converter == null)
                return null;
            var result = _runnable.Run(converter.Convert(parameter));

            converter = _runnable.SupportedArgumentUnwrappers.FirstOrDefault(c => c.IsConvertable(result));

            if (converter == null)
                return null;

            return converter.Convert(result) as Argument;
        }

        public static Executor Create(IRunnable runnable)
        {
            Constraints.NotNull(() => runnable);
            return new Executor(runnable);
        }
    }
}
