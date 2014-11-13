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

            mock.Setup(executor => executor.Execute(It.IsAny<IRunnable>(), It.IsAny<Argument>()))
                .Returns<IRunnable, Argument>((runnable, argument) => {
                    if (argument is IValueArgument)
                    {
                        var valueArgument = argument as IValueArgument;
                        return Arguments.Value(runnable.Run((TArgument)valueArgument.Value));
                    }

                    throw new NotSupportedException("Not supported argument type!");
                });

            return mock.Object;
        }
    }
}
