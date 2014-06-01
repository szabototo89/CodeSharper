using System.Linq;
using CodeSharper.Core;
using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Compilers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core
{
    [TestFixture]
    class AbstractSyntaxTreeTestFixture
    {
        [Test(Description = "AbstractSyntaxTree should manage TextInformationManager")]
        public void AbstractSyntaxTreeShouldManageTextInformationManagerTest()
        {
            // GIVEN
            var underTest = new AbstractSyntaxTree();

            // WHEN
            var result = underTest.TextInformationManager;

            // THEN
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<TextInformationManager>());
        }

        [Test]
        public void GetNodeFromCurrentTextLocationTest()
        {
            // GIVEN
            var source = @"one,two,three";
            var underTest = CsvCompiler.CompileFromString(source).TextInformationManager;

            // WHEN
            var result = underTest.GetNodesByTextLocation(4);

            // THEN
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Any(node => node is FieldNode), Is.True);
            Assert.That(result.OfType<FieldNode>().SingleOrDefault(), Is.Not.Null);
            Assert.That(result.OfType<FieldNode>().Single().Value, Is.EqualTo("two"));
        }
    }
}