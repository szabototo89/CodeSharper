using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core
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

        [TestCase(10, 3, 0, 0)]
        [TestCase(1, 5, 3, 40)]
        [TestCase(10, 5, -4, 40)]
        [Description("TextLocation should be offset by line and column numbers.")]
        public void TextLocationShouldBeOffsetByLineAndColumnTest(int column, int line, int offsetColumn, int offsetLine)
        {
            // GIVEN
            var offset = new {
                Column = offsetColumn,
                Line = offsetLine
            };
            var underTest = new TextLocation(column, line);

            // WHEN
            var result = TextLocationHelper.Offset(underTest, offset.Column, offset.Line);

            // THEN
            var expected = new TextLocation(column + offsetColumn, line + offsetLine);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result, Is.Not.SameAs(underTest));
        }

        [Test(Description = "TextLocation should be offset by another TextLocation")]
        public void TextLocationShouldBeOffsetByAnotherTextLocationTest()
        {
            // GIVEN
            var offset = new TextLocation(10, 3);
            var underTest = new TextLocation();

            // WHEN
            var result = underTest.Offset(offset);

            // THEN
            var expected = new TextLocation(offset.Column + underTest.Column, offset.Line + underTest.Line);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result, Is.Not.SameAs(underTest));
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
    }
}
