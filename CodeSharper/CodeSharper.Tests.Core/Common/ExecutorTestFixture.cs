using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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

            var runnableMock = new Mock<IRunnable<Int32, Int32>>();
            runnableMock.Setup(r => r.Run(It.IsAny<Int32>()))
                .Returns<Int32>(value => value * 2);

            var runnable = runnableMock.Object;

            // When
            var parameter = 20;
            var runnableResult = runnable.Run(parameter);

            var result = new
            {
                IsWrappable = underTest.IsConvertable(parameter),
                Wrap = underTest.Convert(runnableResult),
            };

            // Then
            Assert.That(result.IsWrappable, Is.True);
            Assert.That(result.Wrap, Is.EqualTo(Arguments.Value(40)));
        }

        [Test]
        public void MultiValueArgumentWrapperShouldConvertAnyValueToValueArgument()
        {
            // Given
            var underTest = new MultiValueArgumentWrapper<Int32>();

            var runnableMock = new Mock<IRunnable<Int32, IEnumerable<Int32>>>();
            runnableMock.Setup(r => r.SupportedArgumentConverters)
                .Returns(() => new[] { underTest });
            runnableMock.Setup(r => r.Run(It.IsAny<Int32>()))
                .Returns<Int32>(value => Enumerable.Repeat(value, 3));

            var runnable = runnableMock.Object;

            // When
            var parameter = 20;
            var runnableResult = runnable.Run(parameter);
            var result = new
            {
                IsConvertable = underTest.IsConvertable(runnableResult),
                Value = underTest.Convert(runnableResult)
            };

            // Then
            Assert.That(result.IsConvertable, Is.True);
            Assert.That(result.Value, Is.InstanceOf<MultiValueArgument<Int32>>());
            Assert.That((result.Value as MultiValueArgument<Int32>).Values, Is.EquivalentTo(new[] { 20, 20, 20 }));
        }


    }
}
