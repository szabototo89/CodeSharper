using System;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    public class TextLocationTestFixture
    {
        [Test(Description = "TextLocation should contains line and column information.")]
        public void TextLocationShouldContainsLineAndColumnInformationTest()
        {
            // GIVEN
            var underTest = new TextLocation();

            // WHEN
            var result = new {
                Column = underTest.Column,
                Line = underTest.Line
            };

            // THEN
            Assert.That(result, Is.EqualTo(new {
                Column = 0,
                Line = 0
            }));
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(-20)]
        [Description("TextLocation should throw ArgumentException when initialized with negative column.")]
        public void TextLocationShouldThrowArgumentExceptionWhenInitializedWithNegativeColumnNumberTest(int column)
        {
            // GIVEN
            TestDelegate underTest = () => new TextLocation(column, 10);

            // WHEN
            // THEN
            Assert.That(underTest, Throws.ArgumentException);
        }

        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(-20)]
        [Description("TextLocation should throw ArgumentException when initialized with negative line number.")]
        public void TextLocationShouldThrowArgumentExceptionWhenInitializedWithNegativeLineNumberTest(int line)
        {
            // GIVEN
            TestDelegate underTest = () => new TextLocation(10, line);

            // WHEN
            // THEN
            Assert.That(underTest, Throws.ArgumentException);
        }

        [TestCase(10, 13)]
        [TestCase(18, 4)]
        [Description("TextLocation should be initialized with line and column numbers.")]
        public void TextLocationShouldBeInitializedWithLineAndColumnNumbersTest(int line, int column)
        {
            // GIVEN
            var underTest = new TextLocation(line, column);

            // WHEN
            var result = new {
                Column = underTest.Column,
                Line = underTest.Line
            };

            // THEN
            var expected = new {
                Column = line,
                Line = column
            };
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TextLocationShouldImplementComparableInterfacesTest()
        {
            // GIVEN
            var underTest = new TextLocation();

            // WHEN
            // THEN
            Assert.That(underTest, Is.InstanceOf<IComparable>());
            Assert.That(underTest, Is.InstanceOf<IComparable<TextLocation>>());
        }

        [TestCase(10, 3)]
        [TestCase(0, 0)]
        [TestCase(346, 100)]
        [Description("TextLocation should compare with equal TextLocation")]
        public void TextLocationShouldCompareWithEqualTextLocationTest(int column, int line)
        {
            // GIVEN
            var value = new TextLocation(column, line);
            var underTest = new TextLocation(column, line);

            // WHEN
            var result = underTest.CompareTo(value);

            // THEN
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(12, 24, 0, 1)]
        [TestCase(35, 12, 1, 1)]
        [TestCase(4, 4, 1, 1)]
        [Description("TextLocation should compare with less TextLocation")]
        public void TextLocationShouldCompareWithLessTextLocationTest(int column, int line, int offsetColumn, int offsetLine)
        {
            // GIVEN
            var value = new TextLocation(column, line);
            var underTest = new TextLocation(column + offsetColumn, line + offsetLine);

            // WHEN
            var result = underTest.CompareTo(value);

            // THEN
            Assert.That(result, Is.EqualTo(1));
        }

        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(0, 0, 1, 1)]
        [Description("TextLocation should compare with greater TextLocation")]
        public void TextLocationShouldCompareWithGreaterTextLocationTest(int column, int line, int offsetColumn, int offsetLine)
        {
            // GIVEN
            var value = new TextLocation(column + offsetColumn, line + offsetLine);
            var underTest = new TextLocation(column, line);

            // WHEN
            var result = underTest.CompareTo(value);

            // THEN
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void TextLocationZeroShouldReturnZeroColumnAndLineNumberTest()
        {
            // GIVEN
            var underTest = TextLocation.Zero;

            // WHEN
            var result = underTest;

            // THEN
            Assert.That(result, Is.EqualTo(new TextLocation(0, 0)));
        }

        [Description("TextLocation should return zero distance from itself")]
        [TestCase(0, 0)]
        [TestCase(10, 2)]
        [TestCase(10, 24)]
        public void TextLocationShouldReturnZeroDistanceFromItselfTest(int column, int line)
        {
            // GIVEN
            var underTest = new TextLocation(column, line);

            // WHEN
            var result = underTest.GetDistanceFrom(underTest);

            // THEN
            Assert.That(result, Is.EqualTo(TextLocation.Zero));
        }

        [Description("TextLocation should return distance from another TextLocation")]
        [TestCase(0, 0, 1, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(13, 14, 1, 1)]
        public void TextLocationShouldReturnDistanceFromAnotherTextLocationTest(int column, int line, int columnOffset, int lineOffset)
        {
            // GIVEN
            var location = new TextLocation(column, line);
            var underTest = new TextLocation(column + columnOffset, line + lineOffset);

            // WHEN
            var result = underTest.GetDistanceFrom(location);

            // THEN
            var expected = new TextLocation(columnOffset, lineOffset);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0, 0, 0)]
        public void TextLocationShouldContainIndexValueTest(int column, int line, int index)
        {
            // GIVEN
            var underTest = new TextLocation(column, line, index);

            // WHEN
            var result = underTest.Index;

            // THEN
            Assert.That(result, Is.EqualTo(index));
        }
    }
}
