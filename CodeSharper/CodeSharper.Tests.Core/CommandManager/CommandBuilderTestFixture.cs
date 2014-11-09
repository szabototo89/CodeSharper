

using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Utilities;
using Moq;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.CommandManager
{
    [TestFixture]
    internal class CommandBuilderTestFixture
    {
        private Mock<CommandDescriptor> _descriptorMock;
        private Mock<IRunnable> _runnableMock;

        [SetUp]
        public void Setup()
        {
            _runnableMock = new Mock<IRunnable>();

            _descriptorMock = new Mock<CommandDescriptor>();
            _descriptorMock
                .SetupGet(desc => desc.Name)
                .Returns(() => "Mock Runnable");
            _descriptorMock
                .SetupGet(desc => desc.CommandNames)
                .Returns(() => new[] { "mock", "mock-runnable" });
        }

        [TearDown]
        public void Teardown() { }

        [Test]
        public void CommandBuilderShouldAbleToRegisterCommands()
        {
            // Given
            var commandMock = new Mock<ICommandFactory>();

            var underTest = new StandardCommandManager();

            // When
            underTest.RegisterCommand(commandMock.Object);
        }

        [Test]
        public void CommandBuilderShouldGetRunnableByCommandName()
        {
            // Given
            var underTest = new StandardCommandManager();

            // When
            var result = underTest.TryGetCommandsByName("mock");

            // Then
            Assert.That(result, Is.InstanceOf<Option<IRunnable>>());
        }
    }
}