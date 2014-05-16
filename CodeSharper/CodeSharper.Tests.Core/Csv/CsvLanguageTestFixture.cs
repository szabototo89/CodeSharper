using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using CodeSharper.Core.Csv;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Csv
{
    [TestFixture]
    class CsvLanguageTestFixture
    {
        [Test]
        public void CsvLanguageDescriptorTest()
        {
            // GIVEN
            var underTest = CsvLanguageDescriptor.Default;
            // WHEN
            var result = underTest.Name;
            // THEN
            Assert.That(result, Is.EqualTo("CSV"));
        }

        [Test]
        public void CsvLanguageHasCsvNodeTypeTest()
        {
            // GIVEN
            var underTest = CsvNodeType.Record;
            // WHEN
            // THEN
            Assert.That(underTest, Is.InstanceOf<NodeType>());
        }

        [Test]
        public void CsvNodeTypeDescriptorHasCsvLanguageTest()
        {
            // GIVEN
            var underTest = new CsvNodeTypeDescriptor();
            // WHEN
            var result = underTest.Language;
            // THEN
            Assert.That(result, Is.EqualTo(CsvLanguageDescriptor.Default));
            Assert.That(result, Is.InstanceOf<CsvLanguageDescriptor>());
        }
    }
}
