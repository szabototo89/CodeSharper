using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CodeSharper.Common;
using CodeSharper.Csv;
using NUnit.Framework;

namespace CodeSharper.Tests.Csv
{
    [TestFixture]
    class CsvEmptyNodeTestFixture
    {
        CsvEmptyNode UnderTest { get; set; }

        [SetUp]
        public void SetUp()
        {
            UnderTest = new CsvEmptyNode(TextPosition.Zero, null);
        }

        [Test]
        public void CsvEmptyNodeShouldBeInitalized()
        {
            // GIVEN in setup
            // WHERE
            var result = UnderTest.Text;
            // THEN
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void CsvEmptyNodeShouldNotHaveChildren()
        {
            // GIVEN in setup
            // WHERE
            var result = UnderTest.Children;
            // THEN
            Assert.IsEmpty(result);
        }

        [Test]
        public void CsvEmptyNodeShouldNotHaveSiblings()
        {
            // GIVEN in setup
            // WHERE
            var result = UnderTest.Siblings;
            // THEN
            Assert.IsEmpty(result);
        }
    }
}
