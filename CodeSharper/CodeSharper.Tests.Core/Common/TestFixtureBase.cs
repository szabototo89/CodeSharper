using System;
using Ninject;

namespace CodeSharper.Tests.Core.Common
{
    internal class TestFixtureBase : IDisposable
    {
        protected IKernel Kernel;

        public TestFixtureBase()
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