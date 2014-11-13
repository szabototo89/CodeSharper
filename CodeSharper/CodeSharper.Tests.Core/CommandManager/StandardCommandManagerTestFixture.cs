

using System;
using System.Collections.Generic;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Utilities;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.CommandManager
{
    [TestFixture]
    internal class StandardCommandManagerTestFixture
    {
        [SetUp]
        public void Setup() { }

        [TearDown]
        public void Teardown() { }

        [Test]
        public void StandardCommandManagerShouldAbleToRegisterCommands()
        {
            // Given
            var commandMock = new Mock<ICommandFactory>();
            commandMock
                .SetupGet(_ => _.Descriptor)
                .Returns(() => new CommandDescriptor());

            var underTest = new StandardCommandManager();

            // When
            underTest.RegisterCommandFactory(commandMock.Object);
        }

        [Test]
        public void StandardCommandManagerShouldGetRunnableByCommandName()
        {
            // Given
            var underTest = new StandardCommandManager();

            // When
            var result = underTest.TryGetCommandFactoriesByName("mock");

            // Then
            Assert.That(result, Is.InstanceOf<IEnumerable<ICommandFactory>>());
        }

        [Test]
        public void StandardCommandManagerShouldCreateCommandWithoutArgument()
        {
            // Given
            var factoryMock = new Mock<ICommandFactory>();
            var commandName = "mock";

            factoryMock
                .SetupGet(factory => factory.Descriptor)
                .Returns(() => new CommandDescriptor { CommandNames = new[] { commandName } });

            var underTest = new StandardCommandManager();
            underTest.RegisterCommandFactory(factoryMock.Object);

            // When
            var result = underTest.TryGetCommand(new CommandCallDescriptor(commandName));

            // Then
            Assert.That(result.HasValue, Is.True);
        }

        [Test]
        public void StandardCommandManagerShouldCreateCommandWithNameArguments()
        {
            // Given
            var factoryMock = new Mock<ICommandFactory>();
            var commandName = "mock";

            factoryMock
                .SetupGet(factory => factory.Descriptor)
                .Returns(() => new CommandDescriptor {
                    CommandNames = new[] { commandName },
                    Arguments = new[] { 
                        new NamedArgumentDescriptor(){
                            ArgumentName = "value",
                            ArgumentType =  typeof(Int32),
                            IsOptional = false
                        } 
                    }
                });

            factoryMock
                .Setup(_ => _.CreateCommand(It.IsAny<CommandArgumentCollection>()))
                .Returns<CommandArgumentCollection>(arguments => new Command(null, CommandDescriptor.Empty, arguments));

            var underTest = new StandardCommandManager();
            underTest.RegisterCommandFactory(factoryMock.Object);

            // When
            var result = underTest.TryGetCommand(
                new CommandCallDescriptor(commandName,
                                          namedArguments: new Dictionary<String, Object> { { "value", 10 } }));

            // Then
            Assert.That(result.HasValue, Is.True);
        }
    }
}