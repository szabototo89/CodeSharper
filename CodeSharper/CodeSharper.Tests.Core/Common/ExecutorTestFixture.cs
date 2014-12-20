using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.TestHelpers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ExecutorTestFixture
    {
        [Test(Description = "SimpleValueArgumentWrapper should convert any value to value argument")]
        public void SimpleValueArgumentWrapperShouldConvertAnyValueToValueArgument()
        {
            // Given
            var underTest = new ValueArgumentAfter<Int32>();

            var runnableMock = new Mock<IRunnable<Int32, Int32>>();
            runnableMock.Setup(r => r.Run(It.IsAny<Int32>()))
                .Returns<Int32>(value => value * 2);

            var runnable = runnableMock.Object;

            // When
            var parameter = 20;
            var runnableResult = runnable.Run(parameter);

            var result = new {
                IsWrappable = underTest.IsConvertable(parameter),
                Wrap = underTest.Convert(runnableResult),
            };

            // Then
            Assert.That(result.IsWrappable, Is.True);
            Assert.That(result.Wrap, Is.EqualTo(Arguments.Value(40)));
        }

        [Test(Description = "MultiValueArgumentWrapper should convert any value to value argument")]
        public void MultiValueArgumentWrapperShouldConvertAnyValueToValueArgument()
        {
            // Given
            var underTest = new MultiValueArgumentAfter<Int32>();

            var runnableMock = new Mock<IRunnable<Int32, IEnumerable<Int32>>>();
            runnableMock.Setup(r => r.Run(It.IsAny<Int32>()))
                .Returns<Int32>(value => Enumerable.Repeat(value, 3));

            var runnable = runnableMock.Object;
            TypeDescriptor.AddAttributes(runnable, new ConsumesAttribute(underTest.GetType()));

            // When
            var parameter = 20;
            var runnableResult = runnable.Run(parameter);
            var result = new {
                IsConvertable = underTest.IsConvertable(runnableResult),
                Value = underTest.Convert(runnableResult)
            };

            // Then
            Assert.That(result.IsConvertable, Is.True);
            Assert.That(result.Value, Is.InstanceOf<MultiValueArgument<Int32>>());
            Assert.That((result.Value as MultiValueArgument<Int32>).Values, Is.EquivalentTo(new[] { 20, 20, 20 }));
        }

        [Test(Description = "FlattenArgumentWrapper should flatten multi value arguments")]
        public void FlattenArgumentWrapperShouldFlattenMultiValueArguments()
        {
            // Given
            var underTest = new FlattenArgumentAfter<Int32>();

            var runnableMock = new Mock<IRunnable<IEnumerable<Int32>, IEnumerable<IEnumerable<Int32>>>>();
            runnableMock.Setup(r => r.Run(It.IsAny<IEnumerable<Int32>>()))
                .Returns<IEnumerable<Int32>>(value => Enumerable.Repeat(value, 3));

            var runnable = runnableMock.Object;
            TypeDescriptor.AddAttributes(runnable, new ProducesAttribute(underTest.GetType()));

            // When
            var parameter = new[] { 1 };
            var runnableResult = runnable.Run(parameter);
            var result = new {
                IsConvertable = underTest.IsConvertable(runnableResult),
                Value = underTest.Convert(runnableResult)
            };

            // Then
            Assert.That(result.IsConvertable, Is.True);
            Assert.That(result.Value, Is.InstanceOf<MultiValueArgument<Int32>>());
            Assert.That((result.Value as MultiValueArgument<Int32>).Values, Is.EquivalentTo(new[] { 1, 1, 1 }));
        }

        [Test(Description = "StandardExecutor should handle different run modes of runnables")]
        public void StandardExecutorShouldHandleDifferentRunModesOfRunnables()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abc abc abc"));
            var underTest = Executors.StandardExecutor;

            // When
            var result = underTest.Execute(new SplitStringRunnable(" "), parameter);

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            Assert.That((result as MultiValueArgument<TextRange>).Values, Has.Length.Or.Count.EqualTo(3));
        }

        [Test(Description = "FindTextRunnable should handle multiple values")]
        public void FindTextRunnableShouldHandleMultipleValues()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abc abc abc"));
            var underTest = Executors.StandardExecutor;

            // When
            var result = underTest.Execute(new FindTextRunnable("abc"), parameter);

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            var ranges = (result as MultiValueArgument<TextRange>).Values;
            Assert.That(ranges, Has.Length.Or.Count.EqualTo(3));
            Assert.That(ranges.Select(range => range.Text), Is.All.EqualTo("abc"));
        }

        [Test(Description = "FindTextRunnable should handle enumerable values")]
        public void FindTextRunnableShouldHandleEnumerableValues()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abcdef abcdef abcdef"));
            var findRunnable = Executors.StandardExecutor;
            var underTest = Executors.StandardExecutor;

            // When
            var subResult = findRunnable.Execute(new FindTextRunnable("abcdef"), parameter);
            var result = underTest.Execute(new FindTextRunnable("bcd"), subResult);

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            var ranges = (result as MultiValueArgument<TextRange>).Values.ToArray();
            Assert.That(ranges, Has.Length.EqualTo(3));
            Assert.That(ranges.Select(range => range.Text), Is.All.EqualTo("bcd"));
        }

        [Test(Description = "FindTextRunnable should handle enumerable values multiple times")]
        public void FindTextRunnableShouldHandleEnumerableValuesMultipleTimes()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abcdef abcdef abcdef"));
            var underTest = Executors.StandardExecutor;

            // When
            Argument result = parameter;
            5.Times(() => result = underTest.Execute(new FindTextRunnable("abcdef"), result));

            // Then
            Assert.That(result, Is.InstanceOf<MultiValueArgument<TextRange>>());
            var ranges = (result as MultiValueArgument<TextRange>).Values.ToArray();
            Assert.That(ranges, Has.Length.EqualTo(3));
            Assert.That(ranges.Select(range => range.Text), Is.All.EqualTo("abcdef"));
        }

        [Test(Description = "StandardExecutor should be able to chain multiple runnables")]
        public void StandardExecutorShouldBeAbleToChainMultipleRunnables()
        {
            // Given
            var parameter = Arguments.Value(TestHelper.TextRange("abcdef abcdef abcdef"));
            var find = Executors.StandardExecutor;
            var replace = Executors.StandardExecutor;

            // When
            var subResult = find.Execute(new FindTextRunnable("cde"), parameter);
            var result = replace.Execute(new ReplaceTextRunnable("CD"), subResult);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(parameter.Value.Text, Is.EqualTo("abCDf abCDf abCDf"));
        }
    }
}
