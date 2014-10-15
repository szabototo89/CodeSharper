using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common.Runnables
{
    public class RunnableManager
    {
        private static readonly RunnableManager _runnableManager;

        private readonly Dictionary<Type, RunnableDescriptor> _registeredRunnables;

        static RunnableManager()
        {
            _runnableManager = new RunnableManager();
        }

        public static RunnableManager Instance
        {
            get { return _runnableManager; }
        }

        protected RunnableManager()
        {
            _registeredRunnables = new Dictionary<Type, RunnableDescriptor>();
        }

        public RunnableManager Register(IRunnable runnable)
        {
            Constraints.NotNull(() => runnable);
            return Register(runnable.GetType());
        }

        public RunnableManager Register(Type type)
        {
            if (_registeredRunnables.ContainsKey(type))
                return this;

            _registeredRunnables.Add(type, new RunnableDescriptor(type));

            return this;
        }

        public RunnableDescriptor GetRunnableDescriptor(Type type)
        {
            RunnableDescriptor result;
            _registeredRunnables.TryGetValue(type, out result);
            return result;
        }
    }
}