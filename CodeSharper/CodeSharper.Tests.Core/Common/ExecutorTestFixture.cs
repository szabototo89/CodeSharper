using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.Utilities;
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
                IsWrappable = underTest.IsWrappable(parameter),
                Wrap = underTest.Wrap(runnableResult),
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
            runnableMock.Setup(r => r.Run(It.IsAny<Int32>()))
                .Returns<Int32>(value => Enumerable.Repeat(value, 3));

            var runnable = runnableMock.Object;
            TypeDescriptor.AddAttributes(runnable, new ConsumesAttribute(underTest.GetType()));

            // When
            var parameter = 20;
            var runnableResult = runnable.Run(parameter);
            var result = new
            {
                IsConvertable = underTest.IsWrappable(runnableResult),
                Value = underTest.Wrap(runnableResult)
            };

            // Then
            Assert.That(result.IsConvertable, Is.True);
            Assert.That(result.Value, Is.InstanceOf<MultiValueArgument<Int32>>());
            Assert.That((result.Value as MultiValueArgument<Int32>).Values, Is.EquivalentTo(new[] { 20, 20, 20 }));
        }

        [Test]
        public void FlattenArgumentWrapperShouldFlattenMultiValueArguments()
        {
            // Given
            var underTest = new FlattenArgumentWrapper<Int32>();

            var runnableMock = new Mock<IRunnable<IEnumerable<Int32>, IEnumerable<IEnumerable<Int32>>>>();
            runnableMock.Setup(r => r.Run(It.IsAny<IEnumerable<Int32>>()))
                .Returns<IEnumerable<Int32>>(value => Enumerable.Repeat(value, 3));

            var runnable = runnableMock.Object;
            TypeDescriptor.AddAttributes(runnable, new ProducesAttribute(underTest.GetType()));

            // When
            var parameter = new[] { 1 };
            var runnableResult = runnable.Run(parameter);
            var result = new
            {
                IsConvertable = underTest.IsWrappable(runnableResult),
                Value = underTest.Wrap(runnableResult)
            };

            // Then
            Assert.That(result.IsConvertable, Is.True);
            Assert.That(result.Value, Is.InstanceOf<MultiValueArgument<Int32>>());
            Assert.That((result.Value as MultiValueArgument<Int32>).Values, Is.EquivalentTo(new[] { 1, 1, 1 }));
        }

        [Test]
        public void StandardExecutorShouldHandleDifferentRunModesOfRunnables()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abc abc abc"));
            var underTest = Executors.CreateStandardExecutor(new SplitStringRunnable(" "));

            // When
            var result = underTest.Execute(parameter);

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            Assert.That((result as MultiValueArgument<TextRange>).Values, Has.Length.Or.Count.EqualTo(3));
        }

        [Test]
        public void FindTextRunnableShouldHandleMultipleValues()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abc abc abc"));
            var underTest = Executors.CreateStandardExecutor(new FindTextRunnable("abc"));

            // When
            var result = underTest.Execute(parameter);

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            var ranges = (result as MultiValueArgument<TextRange>).Values;
            Assert.That(ranges, Has.Length.Or.Count.EqualTo(3));
            Assert.That(ranges.Select(range => range.Text), Is.All.EqualTo("abc"));
        }

        [Test]
        public void FindTextRunnableShouldHandleEnumerableValues()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abcdef abcdef abcdef"));
            var findRunnable = Executors.CreateStandardExecutor(new FindTextRunnable("abcdef"));
            var underTest = Executors.CreateStandardExecutor(new FindTextRunnable("bcd"));

            // When
            var subResult = findRunnable.Execute(parameter);
            var result = underTest.Execute(subResult);

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            var ranges = (result as MultiValueArgument<TextRange>).Values.ToArray();
            Assert.That(ranges, Has.Length.EqualTo(3));
            Assert.That(ranges.Select(range => range.Text), Is.All.EqualTo("bcd"));
        }

        [Test]
        public void FindTextRunnableShouldHandleEnumerableValuesMultipleTimes()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abcdef abcdef abcdef"));
            var underTest = Executors.CreateStandardExecutor(new FindTextRunnable("abcdef"));

            // When
            Argument result = parameter;
            5.Times(() => result = underTest.Execute(result));

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            var ranges = (result as MultiValueArgument<TextRange>).Values.ToArray();
            Assert.That(ranges, Has.Length.EqualTo(3));
            Assert.That(ranges.Select(range => range.Text), Is.All.EqualTo("abcdef"));
        }

        [Test]
        public void StandardExecutorShouldBeAbleToChainMultipleRunnables()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abcdef abcdef abcdef"));
            var find = Executors.CreateStandardExecutor(new FindTextRunnable("cde"));
            var replace = Executors.CreateStandardExecutor(new ReplaceTextRunnable("CD"));

            // When
            var subResult = find.Execute(parameter);
            var result = replace.Execute(subResult);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(parameter.Value.Text, Is.EqualTo("abCDf abCDf abCDf"));
        }
    }
}
