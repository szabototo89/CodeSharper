using CodeSharper.Core.Experimental;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Experimental
{
    [TestFixture]
    public class TextPositionTests : TestFixtureBase
    {
        [Test(Description = "CompareTo should return zero when same text positions are tested")]
        public void CompareTo_ShouldReturnZero_WhenSameTextPositionsAreTested()
        {
            // Given
            var underTest = new TextPosition(2, 3);
            var other = new TextPosition(2, 3);

            // When
            var result = underTest.CompareTo(other);

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test(Description = "CompareTo should return negative number when other TextPosition.Line is greater")]
        public void CompareTo_ShouldReturnNegativeNumber_WhenOtherTextPositionLineIsGreater()
        {
            // Given
            var underTest = new TextPosition(2, 3);
            var other = new TextPosition(5, 3);

            // When
            var result = underTest.CompareTo(other);

            // Then
            Assert.That(result, Is.LessThan(0));
        }

        [Test(Description = "CompareTo should return negative number when other TextPosition.Line is lesser")]
        public void CompareTo_ShouldReturnNegativeNumber_WhenOtherTextPositionLineIsLesser()
        {
            // Given
            var underTest = new TextPosition(2, 3);
            var other = new TextPosition(5, 3);

            // When
            var result = underTest.CompareTo(other);

            // Then
            Assert.That(result, Is.LessThan(0));
        }
    }
}