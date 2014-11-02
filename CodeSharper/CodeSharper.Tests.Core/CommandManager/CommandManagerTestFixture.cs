using CodeSharper.Core.Commands;
using Moq;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.CommandManager
{
    [TestFixture]
    internal class CommandManagerTestFixture
    {
        protected StandardKernel Kernel { get; set; }

        [SetUp]
        public void Setup()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<ICommandManager>().To<StandardCommandManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Kernel.Dispose();
        }

        [Test]
        public void CommandManagerShouldAbleToRegisterCommands()
        {
            // Given
            var underTest = Kernel.Get<ICommandManager>();

            // When
            underTest.RegisterCommand<IdentityCommand>();
            var result = underTest.IsCommandRegistered<IdentityCommand>();

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void CommandManagerShouldAbleToUnregisterCommands()
        {
            // Given
            var underTest = Kernel.Get<ICommandManager>();

            // When
            underTest.RegisterCommand<IdentityCommand>();
            underTest.UnregisterCommand<IdentityCommand>();
            var result = underTest.IsCommandRegistered<IdentityCommand>();

            // Then
            Assert.That(result, Is.False);
        }
    }
}