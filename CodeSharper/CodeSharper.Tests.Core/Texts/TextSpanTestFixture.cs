using System;
using System.Linq;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    public class TextSpanTestFixture
    {
        [Test]
        public void TextSpanShouldBeInitializedTest()
        {
            // GIVEN
            var text = "Hello World!";
            var underTest = new TextSpan(text);

            // WHEN
            var result = new {
                Start = underTest.Start,
                Stop = underTest.End,
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
        public void TextSpanHasStartTest()
        {
            // GIVEN
            var underTest = new TextSpan();

            // WHEN
            var result = underTest.Start;

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextSpanHasStopTest()
        {
            // GIVEN
            var underTest = new TextSpan();

            // WHEN
            var result = underTest.End;

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextSpanHasLengthTest()
        {
            // GIVEN
            var underTest = new TextSpan();

            // WHEN
            var result = underTest.Length;

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(6, "b")]
        public void TextSpanShouldFillEmptyGapsWhenAreConcanated(int start, string text)
        {
            // GIVEN
            var textSpan = new TextSpan(start, text);
            var underTest = new TextSpan(4, "a");

            // WHEN
            var result = underTest.Append(textSpan);

            // THEN
            var expected = new TextSpan(4, "a\0b");
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void TextSpanShouldThrowExceptionWhenOffsetTooMuchIntoNegativeDirectionTest()
        {
            // GIVEN
            const string text = "Hello World!";
            var underTest = new TextSpan(text);

            // WHEN
            TestDelegate result = () => underTest.OffsetBy(-10);

            // THEN
            Assert.That(result, Throws.InstanceOf<InvalidOperationException>());
        }

        [TestCase(0, "Hello World!")]
        [TestCase(10, "Hello World!")]
        [TestCase(30, "Hello World!")]
        public void TextSpanShouldOffsetByIndexTest(int start, string text)
        {
            // GIVEN
            var underTest = new TextSpan(start, text);

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
        public void TextSpanShouldAppendTextTest(int start, string text, int appendedStart, string appendedText)
        {
            // GIVEN
            var textSpan = new TextSpan(appendedStart, appendedText);
            var underTest = new TextSpan(start, text);

            // WHEN
            var result = underTest.Append(textSpan);

            // THEN
            Assert.That(result, Is.Not.EqualTo(underTest));
            Assert.That(result.Text, Is.EqualTo(underTest.Text + textSpan.Text));
            Assert.That(result.Start, Is.EqualTo(underTest.Start));
            Assert.That(result.End, Is.EqualTo(underTest.Start + result.Text.Length));
            Assert.That(result.Length, Is.EqualTo(underTest.Length + textSpan.Length));
        }

        [TestCase(0, "Hello ", 10, "World!")]
        [TestCase(10, "Hello ", 10, "World!")]
        public void TextSpanShouldPrependTextTest(int start, string text, int prependedStart, string prependedText)
        {
            // GIVEN
            var textSpan = new TextSpan(prependedStart, prependedText);
            var underTest = new TextSpan(start, text);

            // WHEN
            var result = underTest.Prepend(textSpan);

            // THEN
            Assert.That(result, Is.Not.EqualTo(underTest));
            Assert.That(result.Text, Is.EqualTo(textSpan.Text + underTest.Text));
            Assert.That(result.Start, Is.EqualTo(textSpan.Start));
            Assert.That(result.End, Is.EqualTo(textSpan.Start + result.Text.Length));
            Assert.That(result.Length, Is.EqualTo(textSpan.Length + underTest.Length));
        }

        [TestCase(0, "Hello", 10, " World!")]
        public void TextSpanShouldAppendToTextTest(int start, string text, int offset, string appendedText)
        {
            // GIVEN
            var textSpan = new TextSpan(offset, appendedText);
            var underTest = new TextSpan(start, text);

            // WHEN
            var result = textSpan.AppendTo(underTest);

            // THEN
            var expected = underTest.Append(textSpan);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0, "Hello", 10, " World!")]
        public void TextSpanShouldPrependToTextTest(int start, string text, int offset, string prependedText)
        {
            // GIVEN
            var textSpan = new TextSpan(offset, prependedText);
            var underTest = new TextSpan(start, text);

            // WHEN
            var result = textSpan.PrependTo(underTest);

            // THEN
            var expected = underTest.Prepend(textSpan);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
