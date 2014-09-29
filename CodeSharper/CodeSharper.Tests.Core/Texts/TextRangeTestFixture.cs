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

    }
}
