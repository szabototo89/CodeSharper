using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;
using CodeSharper.Csv;
using NUnit.Framework;

namespace CodeSharper.Tests.Csv
{
    [TestFixture]
    class CsvValueNodeTestFixture
    {
        [Test]
        public void CsvValueNodeShouldBeInitalized()
        {
            // GIVEN
            const string expectedResult = "Hello";
            var underTest = new CsvValueNode(expectedResult, TextPosition.Zero, null);
            // WHERE
            var result = underTest.Text;
            // THEN
            Assert.AreEqual(expectedResult, result);
        }
    }
}
