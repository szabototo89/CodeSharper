using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using NUnit.Framework;

namespace CodeSharper.Tests.Core
{
    [TestFixture]
    public class TextSpanTestFixture
    {
        private TextSpan UnderTest;

        private TextSpan GetTextSpan(TextLocation start, TextLocation stop)
        {
            return new TextSpan(start, stop);
        }

        [SetUp]
        public void Setup()
        {
            var start = new TextLocation(0, 10);
            var stop = new TextLocation(24, 13);
            UnderTest = new TextSpan(start, stop);
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

        [Description("TextSpan should offset by zero TextLocation")]
        [TestCase(0, 0)]
        [TestCase(10, 0)]
        [TestCase(0, 30)]
        [TestCase(10, 30)]
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

        [TestCase(10, 0)]
        public void TextSpanShouldOffsetByTextLocationTest(int column, int line)
        {
            // GIVEN
            var underTest = GetTextSpan(TextLocation.Zero, new TextLocation(column, line));

            // WHEN

            // THEN
        }
    }
}
