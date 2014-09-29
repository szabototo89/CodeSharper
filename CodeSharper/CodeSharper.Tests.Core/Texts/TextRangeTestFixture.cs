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
            // GIVEN
            var text = "Hello World!";
            var underTest = new TextRange(text);

            // WHEN
            var result = new {
                Start = underTest.Start,
                Stop = underTest.Stop,
                Length = underTest.Length
            };

            // THEN
            Assert.That(result, Is.EqualTo(new {
                Start = 0,
                Stop = text.Length,
                Length = text.Length
            }));
        }

        [Test]
        public void TextRangeHasStartTest()
        {
            // GIVEN
            var underTest = new TextRange("");

            // WHEN
            var result = underTest.Start;

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeHasStopTest()
        {
            // GIVEN
            var underTest = new TextRange(string.Empty);

            // WHEN
            var result = underTest.Stop;

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeHasLengthTest()
        {
            // GIVEN
            var underTest = new TextRange(string.Empty);

            // WHEN
            var result = underTest.Length;

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(6, "b")]
        public void TextRangeShouldFillEmptyGapsWhenAreConcanated(int start, string text)
        {
            // GIVEN
            var textRange = new TextRange(start, text);
            var underTest = new TextRange(4, "a");

            // WHEN
            var result = underTest.Append(textRange);

            // THEN
            var expected = new TextRange(4, "a\0b");
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TextRangeShouldThrowExceptionWhenOffsetTooMuchIntoNegativeDirectionTest()
        {
            // GIVEN
            const string text = "Hello World!";
            var underTest = new TextRange(text);

            // WHEN
            TestDelegate result = () => underTest.OffsetBy(-10);

            // THEN
            Assert.That(result, Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase(0, "Hello World!")]
        [TestCase(10, "Hello World!")]
        [TestCase(30, "Hello World!")]
        public void TextRangeShouldOffsetByIndexTest(int start, string text)
        {
            // GIVEN
            var underTest = new TextRange(start, text);

            // WHEN
            var offset = 10;
            var result = underTest.OffsetBy(offset);

            // THEN
            Assert.That(result, Is.Not.EqualTo(underTest));
            Assert.That(result.Start, Is.EqualTo(underTest.Start + offset));
            Assert.That(result.Text, Is.EqualTo(underTest.Text));
        }

        [TestCase(0, "Hello ", 10, "World!")]
        [TestCase(10, "Hello ", 10, "World!")]
        public void TextRangeShouldAppendTextTest(int start, string text, int appendedStart, string appendedText)
        {
            // GIVEN
            var textRange = new TextRange(appendedStart, appendedText);
            var underTest = new TextRange(start, text);

            // WHEN
            var result = underTest.Append(textRange);

            // THEN
            Assert.That(result, Is.Not.EqualTo(underTest));
            Assert.That(result.Text, Is.EqualTo(underTest.Text + textRange.Text));
            Assert.That(result.Start, Is.EqualTo(underTest.Start));
            Assert.That(result.Stop, Is.EqualTo(underTest.Start + result.Text.Length));
            Assert.That(result.Length, Is.EqualTo(underTest.Length + textRange.Length));
        }

        [TestCase(0, "Hello ", 10, "World!")]
        [TestCase(10, "Hello ", 10, "World!")]
        public void TextRangeShouldPrependTextTest(int start, string text, int prependedStart, string prependedText)
        {
            // GIVEN
            var textRange = new TextRange(prependedStart, prependedText);
            var underTest = new TextRange(start, text);

            // WHEN
            var result = underTest.Prepend(textRange);

            // THEN
            Assert.That(result, Is.Not.EqualTo(underTest));
            Assert.That(result.Text, Is.EqualTo(textRange.Text + underTest.Text));
            Assert.That(result.Start, Is.EqualTo(textRange.Start));
            Assert.That(result.Stop, Is.EqualTo(textRange.Start + result.Text.Length));
            Assert.That(result.Length, Is.EqualTo(textRange.Length + underTest.Length));
        }

        [TestCase(0, "Hello", 10, " World!")]
        public void TextRangeShouldAppendToTextTest(int start, string text, int offset, string appendedText)
        {
            // GIVEN
            var textRange = new TextRange(offset, appendedText);
            var underTest = new TextRange(start, text);

            // WHEN
            var result = textRange.AppendTo(underTest);

            // THEN
            var expected = underTest.Append(textRange);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0, "Hello", 10, " World!")]
        public void TextRangeShouldPrependToTextTest(int start, string text, int offset, string prependedText)
        {
            // GIVEN
            var textRange = new TextRange(offset, prependedText);
            var underTest = new TextRange(start, text);

            // WHEN
            var result = textRange.PrependTo(underTest);

            // THEN
            var expected = underTest.Prepend(textRange);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
