using System;
using System.Linq;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    public class TextSpanTestFixture
    {
        private TextSpan GetTextSpan(TextLocation start, TextLocation stop)
        {
            return new TextSpan(start, stop);
        }

        [Test(Description = "TextSpan should initialized with TextLocation")]
        public void TextSpanShouldInitializeWithTextLocationTest()
        {
            // GIVEN
            var start = new TextLocation();
            var stop = new TextLocation();
            var underTest = new TextSpan(start, stop);

            // WHEN
            var result = new { underTest.Start, underTest.Stop };

            // THEN
            var expected = new {
                Start = start,
                Stop = stop
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test(Description = "Start of TextSpan must be less than Stop of TextSpan otherwise it throws ArgumentException.")]
        public void StartOfTextSpanMustBeLessThanStopOfTextSpanOtherwiseItThrowsArgumentExceptionTest()
        {
            // GIVEN
            var start = new TextLocation();
            var stop = new TextLocation(10, 10);

            // WHEN
            TestDelegate result = () => new TextSpan(stop, start);

            // THEN
            Assert.That(result, Throws.ArgumentException);
        }

        [TestCase("")]
        [TestCase("some text")]
        [Description("TextSpan value contains text")]
        public void TextSpanValueContainsTextTest(string text)
        {
            // GIVEN
            var underTest = new TextSpan(text);

            // WHEN
            var result = underTest.Text;

            // THEN
            var expected = text;
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0, 0)]
        [TestCase(10, 0)]
        [TestCase(0, 30)]
        [TestCase(10, 30)]
        [Description("TextSpan should offset by zero TextLocation")]
        public void TextSpanShouldOffsetByZeroTextLocationTest(int column, int line)
        {
            // GIVEN
            var location = TextLocation.Zero;
            var underTest = GetTextSpan(TextLocation.Zero, new TextLocation(column, line));

            // WHEN
            var result = underTest.Offset(location);

            // THEN
            var expected = underTest;
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(10, 0, 1, 1)]
        [TestCase(0, 10, 0, 6)]
        [Description("TextSpan should offset by TextLocation")]
        public void TextSpanShouldOffsetByTextLocationTest(int column, int line, int offsetColumn, int offsetLine)
        {
            // GIVEN
            var location = new TextLocation(offsetColumn, offsetLine);
            var underTest = GetTextSpan(TextLocation.Zero, new TextLocation(column, line));

            // WHEN
            var result = underTest.Offset(location);

            // THEN
            var expected = new TextSpan(underTest.Start.Offset(location),
                                        underTest.Stop.Offset(location));

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("")]
        [TestCase("TextSpan")]
        [Description("TextSpan should be initialized with string literal")]
        public void TextSpanShouldBeInitializedWithStringLiteralTest(string text)
        {
            // GIVEN
            var underTest = TextSpan.FromString(text);

            // WHEN
            var result = new {
                underTest.Start,
                underTest.Stop
            };

            // THEN
            var expected = new {
                Start = TextLocation.Zero,
                Stop = new TextLocation(text.Length, 0, text.Length)
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("Hello\nWorld!")]
        [TestCase("Line 1\nLine 2\n   Line 3!")]
        public void TextSpanShouldBeInitializedWithMultilineStringLiteralTest(string text)
        {
            // GIVEN
            var underTest = TextSpan.FromString(text);

            // WHEN
            var result = new {
                underTest.Start,
                underTest.Stop
            };

            // THEN
            var lines = text.Split(new[] { "\n" }, StringSplitOptions.None);
            var expected = new {
                Start = TextLocation.Zero,
                Stop = new TextLocation(lines.Last().Length, lines.Length - 1, text.Length)
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TextSpanFromStringShouldThrowArgumentNullExceptionWithNullStringTest()
        {
            // GIVEN 
            string underTest = null;

            // WHEN
            TestDelegate result = () => TextSpan.FromString(underTest);

            // THEN
            Assert.That(result, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void TextSpanShouldAppendAnotherTextSpanTest()
        {
            // GIVEN
            var span = TextSpan.FromString("World");
            var underTest = TextSpan.FromString("Hello");

            // WHEN
            var result = underTest.Append(span);

            // THEN
            Assert.That(result.Start, Is.EqualTo(underTest.Start));
            Assert.That(result.Stop, Is.EqualTo(span.Stop));
            Assert.That(result.Text, Is.EqualTo(underTest.Text + span.Text));
        }

    }
}
