using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Runnables.CollectionOperations;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.CollectionRunnables
{
    [TestFixture]
    public class FilterRunnableTests : RunnableTestFixtureBase<FilterRunnable>
    {
        [Test(Description = "Run should return matching string by regular expression when strings are passed")]
        public void Run_ShouldReturnMatchingStringsByRegularExpression_WhenStringsArePassed()
        {
            // Given
            underTest.Pattern = "^[a-z]+$";
            IEnumerable<Object> parameter = new[] {"hello", "world", "!", "123"};

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {"hello", "world"}));
        }

        [Test(Description = "Run should return matching text ranges by regular expression when text ranges are passed")]
        public void Run_ShouldReturnMatchingTextRangesByRegularExpression_WhenTextRangesArePassed()
        {
            // Given
            var document = new TextDocument("hello world!");
            var firstWord = document.CreateOrGetTextRange(0, 5);
            var secondWord = document.CreateOrGetTextRange(7, 11);

            var parameters = new[]
            {
                firstWord, secondWord,
                document.CreateOrGetTextRange(12, 12)
            };

            underTest.Pattern = "^[a-z]+$";

            // When
            var result = underTest.Run(parameters);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { firstWord, secondWord }));
        }
    }
}