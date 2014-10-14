using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using Moq;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ExecutorTestFixture
    {
        [Test]
        public void SimpleValueArgumentWrapperShouldConvertAnyValueToValueArgument()
        {
            // Given
            var underTest = new ValueArgumentWrapper<Int32>();
            var runnableMock = new Mock<Runnable<Int32, Int32>>();

            runnableMock.Setup(r => r.Run(It.IsAny<Int32>()))
                .Returns<Int32>(value => value * 2);

            var runnable = runnableMock.Object;

            // When
            var parameter = 20;
            var runnableResult = runnable.Run(parameter);

            var result = new
            {
                IsWrappable = underTest.IsWrappable(parameter),
                IsUnwrappable = underTest.IsUnwrappable(underTest.Wrap(runnableResult)),
                Wrap = underTest.Wrap(runnableResult),
                Unwrap = underTest.Unwrap(underTest.Wrap(runnableResult))
            };

            // Then
            Assert.That(result.IsWrappable, Is.True);
            Assert.That(result.IsUnwrappable, Is.True);
            Assert.That(result.Wrap, Is.EqualTo(Arguments.Value(40)));
            Assert.That(result.Unwrap, Is.EqualTo(40));
        }

    }
}
