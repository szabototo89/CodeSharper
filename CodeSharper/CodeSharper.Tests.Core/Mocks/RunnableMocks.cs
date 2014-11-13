using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
using Moq;

namespace CodeSharper.Tests.Core.Mocks
{
    public static class RunnableMocks
    {
        public static IRunnable ConstantRunnable(Object value)
        {
            var mock = new Mock<IRunnable>();

            mock.Setup(runnable => runnable.Run(It.IsAny<Object>()))
                .Returns<Object>(arg => value);

            return mock.Object;
        }
    }
}
