using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Commands.CommandFactories;
using Moq;

namespace CodeSharper.Tests.Core.Mocks
{
    public static class CommandFactoryMocks
    {
        public static ICommandFactory ConstantCommandFactory(String commandName, String parameterName = "value")
        {
            var mock = new Mock<ICommandFactory>();

            var descriptor = new CommandDescriptor() {
                CommandNames = new[] { commandName },
                Arguments = new[] {
                    new NamedArgumentDescriptor {ArgumentName = parameterName, ArgumentType = typeof (Object)}
                }
            };

            mock.SetupGet(command => command.Descriptor).Returns(descriptor);

            mock.Setup(factory => factory.CreateCommand(It.IsAny<CommandArgumentCollection>()))
                .Returns<CommandArgumentCollection>(args => {
                    return new Command(RunnableMocks.ConstantRunnable(args.GetArgumentValue<Object>(parameterName)),
                        descriptor, args);
                });

            return mock.Object;
        }

        public static ICommandFactory DelegateCommandFactory(string commandName, Func<Int32, Int32> runnable)
        {
            var mock = new Mock<ICommandFactory>();

            var descriptor = new CommandDescriptor() {
                CommandNames = new[] { commandName },
                Arguments = Enumerable.Empty<ArgumentDescriptorBase>()
            };

            mock.SetupGet(command => command.Descriptor).Returns(descriptor);

            mock.Setup(factory => factory.CreateCommand(It.IsAny<CommandArgumentCollection>()))
                .Returns<CommandArgumentCollection>(args => new Command(RunnableMocks.DelegateRunnable(runnable),descriptor, args));

            return mock.Object;
        }
    }
}
