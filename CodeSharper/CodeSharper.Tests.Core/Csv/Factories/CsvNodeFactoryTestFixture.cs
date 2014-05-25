using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Csv;
using CodeSharper.Core.Csv.Factories;
using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Core.Texts;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Csv.Factories
{
    [TestFixture]
    class CsvNodeFactoryTestFixture
    {
        private CsvTreeFactory UnderTest;

        private FieldNode[] GenerateFields()
        {
            return new[] { UnderTest.Field("one"), UnderTest.Field("two") };
        }

        [SetUp]
        public void Setup()
        {
            UnderTest = new CsvTreeFactory(new CsvAbstractSyntaxTree());
        }

        [Test]
        public void FieldShouldThrowArgumentExceptionTest()
        {
            // GIVEN in setup
            // WHEN
            TestDelegate result = () => UnderTest.Field(null);
            // THEN
            Assert.That(result, Throws.InstanceOf<ArgumentNullException>());
        }

        [TestCase("car")]
        [TestCase("\"1\"")]
        public void FieldShouldCreateFieldNodeTest(string expected)
        {
            // GIVEN in setup
            // WHEN
            var result = UnderTest.Field(expected);
            // THEN
            Assert.That(result, Is.InstanceOf<FieldNode>());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(expected));
        }

        [Test]
        public void RecordShouldCreateRecordNodeTest()
        {
            // GIVEN in setup
            var fields = GenerateFields();

            // WHEN
            var result = UnderTest.Record(fields);

            // THEN
            Assert.That(result, Is.InstanceOf<RecordNode>());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetFields(), Is.EquivalentTo(fields));
        }

        [Test]
        public void RecordShouldThrowArgumentNullExceptionTest()
        {
            // GIVEN in setup
            // WHEN
            TestDelegate result = () => UnderTest.Record(null);
            // THEN
            Assert.That(result, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void CommaShouldCreateCommaNodeTest()
        {
            // GIVEN in setup
            // WHEN
            var result = UnderTest.Comma();
            // THEN
            Assert.That(result, Is.InstanceOf<CommaNode>());
        }

        [Test]
        public void CompilationUnitShouldCreateRecordsTest()
        {
            // GIVEN in setup
            var fields = GenerateFields();
            var records = new[] { UnderTest.Record(fields) };
            // WHEN
            var result = UnderTest.CompilationUnit(records);
            // THEN
            Assert.That(result, Is.InstanceOf<CsvCompilationUnit>());
            Assert.That(result.GetChildren(), Is.EquivalentTo(records));
        }
    }
}
