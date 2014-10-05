using System;
using System.Linq;
using CodeSharper.Core.Texts;
using Moq;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    public class TextRangeTestFixture
    {
        private TextDocument TextDocument;

        [Test]
        public void Setup()
        {
            TextDocument = new TextDocument("Hello World!");
        }

        [Test]
        public void TextRangeShouldBeInitializedTest()
        {
            // Given
            var text = "Hello World!";
            var underTest = new TextRange(text);

            // When
            var result = new
            {
                Start = underTest.Start,
                Stop = underTest.Stop,
                Length = underTest.Length
            };

            // Then
            Assert.That(result, Is.EqualTo(new
            {
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

        [Test(Description = "TextRange should throw exception when Offset too much into negative direction")]
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

        [Test(Description = "TextRange should offet by index")]
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

        [Test]
        public void TextRangeShouldHaveChildren()
        {
            // Given
            var underTest = TextDocument.TextRange;

            // When
            var result = underTest.Children;

            // Then
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void TextRangeShouldBeAbleToDefineSubTextRange()
        {
            // Given
            var textDocument = new TextDocument("Hello");
            var underTest = textDocument.TextRange;

            // When
            var result = underTest.SubStringOfText(1, underTest.Length - 1);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result.Start, Is.EqualTo(1));
            Assert.That(result.Stop, Is.EqualTo(4));
            Assert.That(result.Parent, Is.EqualTo(underTest));
            Assert.That(underTest.Children, Has.Member(result));
        }

    }
}
