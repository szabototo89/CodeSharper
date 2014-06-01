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
                Stop = underTest.Stop
            };

            // THEN
            Assert.That(result, Is.EqualTo(new {
                Start = 0, 
                Stop = text.Length
            }));
        }

        [Test]
        public void TextSpanShouldThrowExceptionWhenOffsetTooMuchIntoNegativeDirectionTest()
        {
            // GIVEN
            string text = "Hello World!";
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
    }
}
