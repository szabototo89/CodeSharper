﻿using System;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public class StandardExecutor : IExecutor
    {
        private readonly IRunnable _runnable;
        private readonly IRunnableManager _runnableManager;
        private RunnableDescriptor _runnableDescriptor;

        public StandardExecutor(IRunnable runnable, IRunnableManager runnableManager)
        {
            Constraints.NotNull(() => runnable);
            _runnable = runnable;
            _runnableManager = runnableManager;
            RegisterRunnable(runnable);
        }

        private StandardExecutor RegisterRunnable(IRunnable runnable)
        {
            _runnableDescriptor = _runnableManager.Register(runnable);
            return this;
        }

        public Argument Execute(Argument parameter)
        {
            return After(Before(parameter));
        }

        private Argument After(Object result)
        {
            var converter = _runnableDescriptor.SupportedArgumentAfters.FirstOrDefault(c => c.IsConvertable(result));

            if (converter == null) return null;
            return converter.Convert(result) as Argument;
        }

        private Object Before(Argument parameter)
        {
            var converter = _runnableDescriptor.SupportedArgumentBefores.FirstOrDefault(c => c.IsConvertable(parameter));

            if (converter == null) return null;
            return converter.Convert(parameter, param => _runnable.Run(param));
        }
    }
}