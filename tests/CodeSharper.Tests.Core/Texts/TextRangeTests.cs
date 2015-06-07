using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    public class TextRangeTests : TestFixtureBase
    {
        [Test(Description = "PositionComparer should return zero when TextRanges are equal by position")]
        public void PositionComparer_ShouldReturnZero_WhenTextRangesAreEqualByPosition()
        {
            // Given
            var firstTextRange = new TextRange(0, 5);
            var secondTextRange = new TextRange(0, 5);

            // When
            var result = TextRange.PositionComparer.Compare(firstTextRange, secondTextRange);

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test(Description = "PositionComparer should return zero when the first TextRange.Stop is lesser than the second one")]
        public void PositionComparer_ShouldReturnMinusOne_WhenTheFirstTextRangeStopIsLesserThanTheSecondOne()
        {
            // Given
            var firstTextRange = new TextRange(0, 3);
            var secondTextRange = new TextRange(0, 5);

            // When
            var result = TextRange.PositionComparer.Compare(firstTextRange, secondTextRange);

            // Then
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test(Description = "PositionComparer should return zero when the second TextRange.Stop is lesser than the first one")]
        public void PositionComparer_ShouldReturnOne_WhenTheSecondTextRangeStopIsLesserThanTheFirstOne()
        {
            // Given
            var firstTextRange = new TextRange(0, 5);
            var secondTextRange = new TextRange(0, 3);

            // When
            var result = TextRange.PositionComparer.Compare(firstTextRange, secondTextRange);

            // Then
            Assert.That(result, Is.EqualTo(1));
        }

        [Test(Description = "PositionComparer should return zero when the second TextRange.Stop is lesser than the first one")]
        public void PositionComparer_ShouldReturnOne_WhenTheFirstContainsTheSecondOne()
        {
            // Given
            var firstTextRange = new TextRange(0, 5);
            var secondTextRange = new TextRange(1, 3);

            // When
            var result = TextRange.PositionComparer.Compare(firstTextRange, secondTextRange);

            // Then
            Assert.That(result, Is.EqualTo(1));
        }

        [Test(Description = "PositionComparer should return zero when the second TextRange.Stop is lesser than the first one")]
        public void PositionComparer_ShouldReturnMinusOne_WhenTheSecondContainsTheFirstOne()
        {
            // Given
            var firstTextRange = new TextRange(1, 3);
            var secondTextRange = new TextRange(0, 5);

            // When
            var result = TextRange.PositionComparer.Compare(firstTextRange, secondTextRange);

            // Then
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test(Description = "SortedSet should store TextRanges by position when position comparer is used")]
        public void SortedSet_ShouldStoreTextRangesByPosition_WhenPositionComparerIsUsed()
        {
            // Given
            var text = "one,two,three,four";
            var textRanges = new[]
            {
                new TextRange(0, 3),
                new TextRange(5, 7),
                new TextRange(9, 13),
                new TextRange(15, 18),
            };

            var underTest = new SortedList<TextRange, TextRange>(TextRange.PositionComparer);

            // When
            foreach (var textRange in textRanges.Reverse())
            {
                underTest.Add(textRange, textRange);
            }

            // Then
            var actual = underTest.Values.SequenceEqual(textRanges);
            Assert.That(actual, Is.True);
        }
    }
}