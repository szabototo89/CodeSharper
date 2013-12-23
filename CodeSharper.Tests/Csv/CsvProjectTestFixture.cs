using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;
using CodeSharper.Csv;
using NUnit.Framework;

namespace CodeSharper.Tests.Csv
{
    public class CsvProject
    {
        public CsvProject(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            Text = text;
            Node = this.Parse(text);
        }

        private ICsvNode Parse(string text)
        {
            return new CsvEmptyNode(TextPosition.Zero, null);
        }

        public string Text { get; protected set; }
        public ICsvNode Node { get; protected set; }
    }

    [TestFixture]
    class CsvProjectTestFixture
    {
        [Test]
        public void CsvProjectShouldBeInitalized()
        {
            // GIVEN
            string expectedValue = "Hello,World";
            var underTest = new CsvProject(expectedValue);
            // WHERE
            string result = underTest.Text;
            // THEN
            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CsvProjectIsInitalizedWithNull()
        {
            // GIVEN
            var underTest = new CsvProject(null);
            // WHERE
            // THEN
        }

        [Test]
        public void CsvProjectShouldReturnEmptyCsvNode()
        {
            // GIVEN
            var underTest = new CsvProject(string.Empty);
            // WHERE
            var result = underTest.Node as CsvEmptyNode;
            // THEN
            Assert.NotNull(result);
        }

        [Test]
        public void CsvProjectShouldReturnNotEmptyCsvNode()
        {
            // GIVEN
            var underTest = new CsvProject("Hello,World");
            // WHERE
            var result = underTest.Node;
            // THEN
            Assert.IsFalse(result is CsvEmptyNode);
        }
    }
}
