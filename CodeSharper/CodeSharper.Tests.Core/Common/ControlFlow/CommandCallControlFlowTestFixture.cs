using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlow;
using CodeSharper.Core.Common.Values;
using CodeSharper.Tests.Core.Mocks;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.ControlFlow
{
    [TestFixture]
    internal class CommandCallControlFlowTestFixture : TestFixtureBase
    {
        [Test(Description = "CommandCallControlFlow should be able to execute any command when pass executor and command")]
        public void CommandCallControlFlowShouldBeAbleToExecuteAnyCommandWhenPassExecutorAndCommand()
        {
            // Given
            var executor = ExecutorMocks.SimpleExecutor<Int32>();
            var command = CommandFactoryMocks.DelegateCommandFactory("test", num => num + 1)
                .CreateCommand(new CommandArgumentCollection());

            var underTest = new CommandCallControlFlow(command, executor);

            // When
            var result = underTest.Execute(Arguments.Value(10));

            // Then
            Assert.That(result, Is.InstanceOf<ValueArgument<Object>>());
            Assert.That(((ValueArgument<Object>)result).Value, Is.EqualTo(11));
        }

        [Test(Description = "CommandCallControlFlow should have default executor when it is initialized")]
        public void CommandCallControlFlowShouldHaveDefaultExecutorWhenItIsInitialized()
        {
            // Given
            var command = CommandFactoryMocks.DelegateCommandFactory("test", num => num)
                                .CreateCommand(new CommandArgumentCollection());

            var underTest = new CommandCallControlFlow(command);

            // When
            var executor = underTest.Executor;

            // Then
            Assert.That(executor, Is.Not.Null);
            Assert.That(executor, Is.InstanceOf<StandardExecutor>());
        }
    }
}
