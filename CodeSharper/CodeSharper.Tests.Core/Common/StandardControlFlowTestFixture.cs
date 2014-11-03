using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ControlFlow;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class StandardControlFlowTestFixture
    {
        private ValueArgument<TValue> Value<TValue>(TValue value)
        {
            return new ValueArgument<TValue>(value);
        }

        private MultiValueArgument<TValue> MultiValue<TValue>(IEnumerable<TValue> value)
        {
            return new MultiValueArgument<TValue>(value);
        }


        private TextRange TextRange(String text)
        {
            return new TextDocument(text).TextRange;
        }

        [Test]
        public void StandardControlFlowShouldHandleSingleExecutor()
        {
            // Given
            StandardControlFlow underTest = ControlFlows.CreateStandardControlFlow();
            underTest.SetControlFlow(new IRunnable[] {
                new SplitStringRunnable(" "),
                new ToUpperCaseRunnable()
            });

            // When
            var result = underTest.Execute(
                Value(TextRange("hello world!"))
            ) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Values.Select(value => value.Text), Is.EquivalentTo(new[] { "HELLO", "WORLD!" }));
        }

        [Test]
        public void StandardControlFlowShouldHandleMultipleValues()
        {
            // Given
            StandardControlFlow underTest = ControlFlows.CreateStandardControlFlow();
            underTest.SetControlFlow(new IRunnable[]
            {
                new SplitStringRunnable(" "),
                new ToUpperCaseRunnable()
            });

            // When
            var result = underTest.Execute(
                MultiValue(Enumerable.Repeat(TextRange("hello world!"), 2))
            ) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Values.Select(value => value.Text), Is.EquivalentTo(new[] { "HELLO", "WORLD!", "HELLO", "WORLD!" }));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(500)]
        public void PerformanceTest(Int32 count)
        {
            // Given
            StandardControlFlow underTest = ControlFlows.CreateStandardControlFlow();
            underTest.SetControlFlow(new[]
            {
                new RegularExpressionRunnable("\\w+")
            });

            // When
            var parameter = Enumerable.Repeat(TextRange("hello world!"), count);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = underTest.Execute(
                MultiValue(parameter)
            ) as MultiValueArgument<TextRange>;
            stopWatch.Stop();

            Debug.WriteLine("[{0}]: {1}", count, stopWatch.Elapsed);
            // Then
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        [TestCase("Hello World!")]
        [TestCase(TestHelper.LoremIpsum)]
        public void StandardControlFlowShouldSplitAndFilterByRegularExpressionAndConvertedToUpperCase(String text)
        {
            // Given
            StandardControlFlow underTest = ControlFlows.CreateStandardControlFlow();
            underTest.SetControlFlow(new IRunnable[] {
                new SplitStringRunnable(" "),
                new RegularExpressionRunnable(@"\w{7,}"), 
                new ToUpperCaseRunnable()
            });

            // When
            var textRange = TextRange(text);
            var result = underTest.Execute(
                Value(textRange)
            ) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            var expected = text
                .Split(new[] { " ", ",", "." }, StringSplitOptions.None)
                .Where(word => word.Length >= 7)
                .Select(word => word.ToUpperInvariant());

            Assert.That(result.Values.Select(range => range.Text), Is.EquivalentTo(expected));
        }

    }
}
