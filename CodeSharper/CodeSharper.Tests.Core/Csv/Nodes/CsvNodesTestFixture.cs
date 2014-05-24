using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Csv;
using CodeSharper.Core.Csv.Factories;
using CodeSharper.Core.Csv.Nodes;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Csv.Nodes
{
    [TestFixture]
    class CsvNodesTestFixture
    {
        private CsvTreeFactory UnderTest;

        [SetUp]
        public void Setup()
        {
            UnderTest = new CsvTreeFactory();
        }

        [Test]
        public void FieldNodeShouldGetNodeTypeDescriptorTest()
        {
            // GIVEN in setup
            // WHEN
            var result = UnderTest.Field(string.Empty)
                                  .GetNodeTypeDescriptor();
            // THEN
            Assert.That(result.Language, Is.EqualTo(CsvLanguageDescriptor.Default));
            Assert.That(result.Type, Is.EqualTo(CsvNodeType.Field));
        }

        [Test]
        public void RecordNodeShouldGetNodeTypeDescriptorTest()
        {
            // GIVEN in setup
            var fields = Enumerable.Empty<FieldNode>();
            // WHEN
            var result = UnderTest.Record(fields)
                                  .GetNodeTypeDescriptor();
            // THEN
            Assert.That(result.Language, Is.EqualTo(CsvLanguageDescriptor.Default));
            Assert.That(result.Type, Is.EqualTo(CsvNodeType.Record));
        }

        [Test]
        public void DelimiterOfCommaNodeShouldReturnCommaTest()
        {
            // GIVEN
            var underTest = new CommaNode();

            // WHEN
            var result = underTest.Delimiter;

            // THEN
            Assert.That(result, Is.EqualTo(","));
        }

        [Test]
        public void CommaNodeShouldGetNodeTypeDescriptorTest()
        {
            // GIVEN in setup
            // WHEN
            var result = UnderTest.Comma()
                                  .GetNodeTypeDescriptor();
            // THEN
            Assert.That(result.Language, Is.EqualTo(CsvLanguageDescriptor.Default));
            Assert.That(result.Type, Is.EqualTo(CsvNodeType.Delimiter));
        }
    }
}
