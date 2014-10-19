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
        private readonly RunnableManager _runnableManager;
        private RunnableDescriptor _runnableDescriptor;

        public StandardExecutor(IRunnable runnable)
        {
            Constraints.NotNull(() => runnable);
            _runnable = runnable;
            _runnableManager = RunnableManager.Instance;
            RegisterRunnable(runnable);
        }

        private StandardExecutor RegisterRunnable(IRunnable runnable)
        {
            _runnableDescriptor = _runnableManager.Register(runnable);
            return this;
        }

        public Argument Execute(Argument parameter)
        {
            return WrapResult(UnwrapParameter(parameter));
        }

        private Argument WrapResult(Object result)
        {
            var unwrapper = _runnableDescriptor.SupportedArgumentWrappers.FirstOrDefault(c => c.IsWrappable(result));

            if (unwrapper == null) return null;
            return unwrapper.Wrap(result) as Argument;
        }

        private Object UnwrapParameter(Argument parameter)
        {
            var converter = _runnableDescriptor.SupportedArgumentUnwrappers.FirstOrDefault(c => c.IsUnwrappable(parameter));

            if (converter == null) return null;
            return converter.Unwrap(parameter, unwrapped => _runnable.Run(unwrapped));
        }
    }
}