using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Csv.Factories;
using CodeSharper.Core.Csv.Nodes;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Csv.Factories
{
    [TestFixture]
    class CsvTreeFactoryTestFixture
    {
        private CsvTreeFactory UnderTest;

        [SetUp]
        public void Setup()
        {
            UnderTest = new CsvTreeFactory();
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
            var fields = new[] { UnderTest.Field("one"), UnderTest.Field("two") };

            // WHEN
            var result = UnderTest.Record(fields);

            // THEN
            Assert.That(result, Is.InstanceOf<RecordNode>());
            Assert.That(result.GetChildren(), Is.EquivalentTo(fields));
        }
    }
}
