using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class RunnableWithStandardExecutorTestFixture : TextRangeTestBase
    {
        private static void CheckRunnableWithSingleValue<TIn, TOut>(Argument argument, IRunnable<TIn, TOut> runnable)
        {
            // Given in parameters

            // When
            var result = Executors.CreateStandardExecutor(runnable)
                .Execute(argument);

            // Then
            Assert.That(result, Is.Not.Null);
        }

        private static void CheckRunnableWithMultipleValues<TIn, TOut>(Argument parameter, IRunnable<TIn, TOut> runnable)
        {
            // Given in parameter

            // When
            var result = Executors.CreateStandardExecutor(runnable)
                .Execute(parameter);

            // Then
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void RunnableShouldAbleToHandleSingleValue()
        {
            Func<Argument> argument = () => Arguments.Value(TextRange("abcdef abcdef"));

            CheckRunnableWithSingleValue(
                argument(), new FindTextRunnable("a"));

            CheckRunnableWithSingleValue(
                argument(), new ReplaceTextRunnable(""));

            CheckRunnableWithSingleValue(
                argument(), new FillStringRunnable("abc"));

            CheckRunnableWithSingleValue(
                argument(), new RepeatRunnable(3));

            CheckRunnableWithSingleValue(
                argument(), new IdentityRunnable());

            CheckRunnableWithSingleValue(
                argument(), new FilterTextByLine(0, " "));
        }

        [Test]
        public void RunnableShouldAbleToHandleMultipleValues()
        {
            Func<Argument> argument = () => Arguments.MultiValue(new[] { TextRange("abcdef abcdef") });
            CheckRunnableWithMultipleValues(
                argument(), new FindTextRunnable("a"));

            CheckRunnableWithMultipleValues(
                argument(), new ReplaceTextRunnable(""));

            CheckRunnableWithMultipleValues(
                argument(), new FillStringRunnable("abc"));

            CheckRunnableWithSingleValue(
                argument(), new IdentityRunnable());
            
            CheckRunnableWithSingleValue(
                argument(), new FilterTextByLine(0, " "));
        }

        [Test]
        public void StringTransformationRunnablesShouldAbleToHandleSingleValue()
        {
            var argument = Arguments.Value(TextRange("abcdef abcdef"));
            CheckRunnableWithSingleValue(
                argument, new ToUpperCaseRunnable());

            CheckRunnableWithSingleValue(
                argument, new ToLowerCaseRunnable());

            CheckRunnableWithSingleValue(
                argument, new RegularExpressionRunnable(".+"));
        }


        [Test]
        public void StringTransformationRunnableShouldAbleToHandleMultipleValues()
        {
            var ranges = Arguments.MultiValue(new[] { TextRange("abcdef abcdef") });
            CheckRunnableWithMultipleValues(
                ranges,
                new ToUpperCaseRunnable());

            CheckRunnableWithMultipleValues(
                ranges,
                new ToLowerCaseRunnable());

            CheckRunnableWithMultipleValues(
                ranges,
                new RegularExpressionRunnable(".+"));
        }

    }
}