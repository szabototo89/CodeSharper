using System;
using Ninject;

namespace CodeSharper.Tests.Core.Common
{
    internal abstract class TestFixtureBase : IDisposable
    {
        protected IKernel Kernel;

        protected TestFixtureBase()
        {
            Kernel = new StandardKernel();
        }

        public void Dispose()
        {
            if (!Kernel.IsDisposed)
                Kernel.Dispose();
        }
    }
}