

using CodeSharper.Core.Commands.CommandFactories;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class AutoCommandFactoryTestFixture
    {
        [SetUp]
        public void Setup() { }

        [TearDown]
        public void Teardown() { }

        [Test(Description = "AutoCommandFactory should be able to bind arguments automatically")]
        public void AutoCommandFactoryShouldBeAbleToBindArgumentsAutomatically()
        {
            // Given
            var underTest = new AutoCommandFactory<InsertTextRangeRunnable>();

            // When

            // Then

        }

    }
}