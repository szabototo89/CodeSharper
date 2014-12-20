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
            var result = Executors.StandardExecutor
                .Execute(runnable, argument);

            // Then
            Assert.That(result, Is.Not.Null);
        }

        private static void CheckRunnableWithMultipleValues<TIn, TOut>(Argument parameter, IRunnable<TIn, TOut> runnable)
        {
            // Given in parameter

            // When
            var result = Executors.StandardExecutor
                .Execute(runnable, parameter);

            // Then
            Assert.That(result, Is.Not.Null);
        }

        [Test(Description = "Runnable should able to handle single value")]
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
                argument(), new FilterTextByLineRunnable(0, " "));

            CheckRunnableWithSingleValue(
                argument(), new FilterTextByColumnRunnable(0));
        }

        [Test(Description = "Runnable should able to handle multiple values")]
        public void RunnableShouldAbleToHandleMultipleValues()
        {
            Func<Argument> argument = () => Arguments.MultiValue(new[] { TextRange("abcdef abcdef") });
            CheckRunnableWithMultipleValues(
                argument(), new FindTextRunnable("a"));

            CheckRunnableWithMultipleValues(
                argument(), new ReplaceTextRunnable(""));

            CheckRunnableWithMultipleValues(
                argument(), new FillStringRunnable("abc"));

            CheckRunnableWithMultipleValues(
                argument(), new IdentityRunnable());

            CheckRunnableWithMultipleValues(
                argument(), new FilterTextByLineRunnable(0, " "));

            CheckRunnableWithMultipleValues(
                argument(), new FilterTextByColumnRunnable(0));
        }

        [Test(Description = "StringTransformationRunnables should able to handle single value")]
        public void StringTransformationRunnablesShouldAbleToHandleSingleValue()
        {
            var argument = Arguments.Value(TextRange("abcdef abcdef"));
            CheckRunnableWithSingleValue(
                argument, new ToUpperCaseRunnable());

            CheckRunnableWithSingleValue(
                argument, new ToLowerCaseRunnable());

            CheckRunnableWithSingleValue(
                argument, new RegularExpressionRunnable(".+"));

            CheckRunnableWithSingleValue(
                argument, new InsertTextRangeRunnable(0, ""));

            CheckRunnableWithSingleValue(
                argument, new TrimTextRangeRunnable());
        }


        [Test(Description = "StringTransformationRunnable should able to handle multiple values")]
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

            CheckRunnableWithMultipleValues(
                ranges,
                new InsertTextRangeRunnable(0, ""));

            CheckRunnableWithMultipleValues(
                ranges,
                new TrimTextRangeRunnable());
        }

    }
}