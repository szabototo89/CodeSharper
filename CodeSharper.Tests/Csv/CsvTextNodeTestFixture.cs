using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeSharper.Common;
using CodeSharper.Csv;
using NUnit.Framework;

namespace CodeSharper.Tests.Csv
{
    [TestFixture]
    class CsvTextNodeTestFixture
    {
        [Test]
        public void CsvTextNodeShouldBeInitialized()
        {
            // GIVEN
            var expected = string.Empty;
            var underTest = new CsvTextNode(expected);
            // WHEN
            var result = underTest.Text;
            // THEN
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CsvTextNodeShouldNotHaveEmptyChildren()
        {
            // GIVEN
            var underTest = new CsvTextNode("Hello");
            // WHEN
            var result = underTest.Children;
            // THEN
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void CsvTextNodeShouldParseMultiplyValues()
        {
            // GIVEN
            var underTest = new CsvTextNode("Hello,World");
            // WHEN
            var result = underTest.Children.OfType<CsvValueNode>();
            // THEN
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void CsvTextNodeShouldParseCommas()
        {
            // GIVEN
            var underTest = new CsvTextNode("Hello,World,\"Hi,World\"");
            // WHEN
            var result = underTest.Children.OfType<CsvCommaNode>();
            // THEN
            Assert.AreEqual(2, result.Count());
        }

        [Test(Description = "Values of CSV text node should be well positioned")]
        public void ValuesOfCsvTextNodeShouldBeWellPositioned()
        {
            // GIVEN
            var underTest = new CsvTextNode("Hello,World,!");
            // WHEN
            var result = underTest.Values.Select(value => value.Span.Start);
            // THEN
            Assert.AreEqual(
                result,
                new[] {
                    new TextPosition(0, 0),
                    new TextPosition(0, 6),
                    new TextPosition(0, 12)
                });
        }

        [Test(Description = "Commas of CSV text node should be well positioned")]
        public void CommasOfCsvTextNodeShouldBeWellPositioned()
        {
            // GIVEN
            var underTest = new CsvTextNode("Hello,World,!");
            // WHEN
            var result = underTest.Children.Select(child => child.Span.Start);
            // THEN
            Assert.AreEqual(
                result,
                new[] {
                    new TextPosition(0, 0),
                    new TextPosition(0, 5),
                    new TextPosition(0, 6),
                    new TextPosition(0, 11),
                    new TextPosition(0, 12)
                });
        }
    }
}