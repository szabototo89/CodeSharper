using System;
using System.Linq;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    public class TextRangeTestFixture
    {
        [Test]
        public void TextRangeShouldBeInitializedTest()
        {
            // Given
            var text = "Hello World!";
            var underTest = new TextRange(text);

            // When
            var result = new {
                Start = underTest.Start,
                Stop = underTest.Stop,
                Length = underTest.Length
            };

            // Then
            Assert.That(result, Is.EqualTo(new {
                Start = 0,
                Stop = text.Length,
                Length = text.Length
            }));
        }

        [Test]
        public void TextRangeHasStartTest()
        {
            // Given
            var underTest = new TextRange(String.Empty);

            // When
            var result = underTest.Start;

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeHasStopTest()
        {
            // Given
            var underTest = new TextRange(String.Empty);

            // When
            var result = underTest.Stop;

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeHasLengthTest()
        {
            // Given
            var underTest = new TextRange(String.Empty);

            // When
            var result = underTest.Length;

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeShouldThrowExceptionWhenOffsetTooMuchIntoNegativeDirectionTest()
        {
            // Given
            const string text = "Hello World!";
            var underTest = new TextRange(text);

            // When
            TestDelegate result = () => underTest.OffsetBy(-10);

            // Then
            Assert.That(result, Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase(0, 10, "Hello World!", 10, 22)]
        [TestCase(10, 5, "Hello World!", 15, 27)]
        [TestCase(30, -20, "Hello World!", 10, 22)]
        public void TextRangeShouldOffsetByIndexTest(Int32 start, Int32 offset, String text, Int32 expectedStart, Int32 expectedStop)
        {
            // Given
            var underTest = new TextRange(start, text);

            // When
            var result = underTest.OffsetBy(offset);

            // Then
            Assert.That(result, Is.EqualTo(underTest));
            Assert.That(result.Start, Is.EqualTo(expectedStart));
            Assert.That(result.Stop, Is.EqualTo(expectedStop));
            Assert.That(result.Text, Is.EqualTo(underTest.Text));
        }

    }
}
