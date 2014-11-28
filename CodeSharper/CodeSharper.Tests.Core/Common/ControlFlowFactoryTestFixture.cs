using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ControlFlow;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ControlFlowFactoryTestFixture : TestFixtureBase
    {
        [SetUp]
        public void Setup() { }

        [TearDown]
        public void Teardown() { }

        [Test]
        public void StandardControlFlowFactoryShouldAbleToParseCommandCallAndBuildAnControlFlowFromIt()
        {
            // Given
            ICommandCall commandCall = ;
            var underTest = new StandardControlFlowFactory();

            // When
            var result = underTest.Parse(commandCall);

            // Then
            Assert.That(result, Is.Not.Null);

        }

    }

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
