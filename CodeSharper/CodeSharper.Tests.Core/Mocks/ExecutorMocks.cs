using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using Moq;

namespace CodeSharper.Tests.Core.Mocks
{
    public static class ExecutorMocks
    {
        public static IExecutor SimpleExecutor<TArgument>()
        {
            var mock = new Mock<IExecutor>();

            mock.Setup(executor => executor.Execute(It.IsAny<IRunnable>(), It.IsAny<ValueArgument<TArgument>>()))
                .Returns<IRunnable, ValueArgument<TArgument>>((runnable, argument) => {
                    return Arguments.Value(runnable.Run(argument.Value));
                });

            return mock.Object;
        }
    }
}
