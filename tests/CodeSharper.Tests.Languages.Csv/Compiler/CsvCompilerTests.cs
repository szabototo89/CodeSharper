using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.SyntaxTrees;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.Csv.Compiler
{
    [TestFixture]
    internal class CsvCompilerTests : TestFixtureBase
    {
        private static String getTextFromNode(Node node)
        {
            return node.TextRange.TextDocument.GetText(node.TextRange);
        }

        [Test(Description = "Parse should create parse tree when valid input is passed")]
        public void Parse_ShouldCreateParseTree_WhenValidInputIsPassed()
        {
            // Given
            var input = "one,two\nthree,four";
            var underTest = new CsvCompiler();

            // When
            var node = underTest.Parse(input) as CsvDocumentNode;
            var fields = node.ToEnumerable().OfType<FieldNode>();
            var allOfThemIsTextField = fields.All(field => field.IsTextField);
            var fieldValues = fields.Select(getTextFromNode);

            // Then
            Assert.That(allOfThemIsTextField, Is.True);
            Assert.That(fieldValues, Is.EquivalentTo(new[]
            {
                "one", "two", "three", "four"
            }));
        }

        [Test(Description = "Parse should able to parse mixed typed fields when valid input is passed")]
        public void Parse_ShouldAbleToParseMixedTypedFields_WhenValidInputIsPassed()
        {
            // Given
            var input = "\"1\",2,,\"4\",5";
            var underTest = new CsvCompiler();

            // When
            var node = underTest.Parse(input).As<CsvDocumentNode>();
            var fields = node.ToEnumerable().OfType<FieldNode>();

            var stringFields = fields.Where(field => field.IsStringField)
                                     .Select(getTextFromNode);

            var textFields = fields.Where(field => field.IsTextField)
                                   .Select(getTextFromNode);

            var emptyFields = fields.Where(field => field.IsEmptyField)
                                    .Select(getTextFromNode);

            // Then
            Assert.That(stringFields, Is.EquivalentTo(new[] { "\"1\"", "\"4\"" }));
            Assert.That(textFields, Is.EquivalentTo(new[] { "2", "5" }));
            Assert.That(emptyFields, Is.EquivalentTo(new[] { "" }));
        }
    }
}
