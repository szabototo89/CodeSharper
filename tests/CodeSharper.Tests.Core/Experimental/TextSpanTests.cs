using CodeSharper.Core.Experimental;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Experimental
{
    [TestFixture]
    internal class TextSpanTests : TestFixtureBase
    {
        [Test(Description = "TextSpan should be initialized with two TextPosition when start is lesser than stop.")]
        public void TextSpan_ShouldBeInitializedWithTwoTextPosition_WhenStartIsLesserThanStop()
        {
            // Given
            var start = new TextPosition(0, 3);
            var stop = new TextPosition(1, 3);
            var underTest = new TextSpan(start, stop);

            // Then
            var expected = new TextSpan(new TextPosition(0, 3), new TextPosition(1, 3));
            Assert.That(underTest, Is.EqualTo(expected));
        }

        [Test(Description = "TextSpan should throw ArgumentException when start is greater than stop.")]
        public void TextSpan_ShouldThrowArgumentException_WhenStartIsGreaterThanStop()
        {
            // Given
            var start = new TextPosition(5, 4);
            var stop = new TextPosition(2, 4);

            // When
            TestDelegate initialization = () => new TextSpan(start, stop);

            // Then
            Assert.That(initialization, Throws.ArgumentException);
        }
    }
}