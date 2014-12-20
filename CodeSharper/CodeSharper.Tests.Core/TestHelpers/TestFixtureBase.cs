using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.TestHelpers
{
    /// <summary>
    /// Abstract class for testing
    /// </summary>
    internal abstract class TestFixtureBase : IDisposable
    {
        protected IKernel Kernel;

        protected TestFixtureBase()
        {
            Kernel = new StandardKernel();
        }

        public virtual void Dispose()
        {
            if (!Kernel.IsDisposed)
                Kernel.Dispose();
        }

        [SetUp]
        public virtual void Setup() { }

        [TearDown]
        public virtual void Teardown() { }
    }
}
