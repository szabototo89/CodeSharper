using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Factories;
using CodeSharper.Languages.Csv.SyntaxTrees;
using CodeSharper.Languages.Csv.Visitors;
using CodeSharper.Tests.Languages.Csv.Fakes;
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
            var node = underTest.Parse(input) as CsvCompilationUnit;
            var fields = node.ToEnumerable().OfType<FieldSyntax>();
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
            var node = underTest.Parse(input).As<CsvCompilationUnit>();
            var fields = node.ToEnumerable().OfType<FieldSyntax>();

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

        [Test(Description = "Parse should able to parse multiple rows when valid input is passed")]
        public void Parse_ShouldAbleToParseMultipleRows_WhenValidInputIsPassed()
        {
            // Given
            var input = "1,2,3\n4,5,6\n";
            var visitor = new CsvTreeVisitorStub(CsvLanguageElements.Row);
            var treeFactory = new CsvTreeFactoryStub();
            var underTest = new CsvCompiler();

            // When
            underTest.Parse(input, visitor, treeFactory);
            var result = visitor.GetVisitedRules();

            // Then
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Contains.Item("Row(1,2,3\n)"));
            Assert.That(result, Contains.Item("Row(4,5,6\n)"));
        }

        #region Using CsvStandardSyntaxTreeBuilder for parsing CSV file

        [Test(Description = "Parse should create parse tree when CsvStandardSyntaxTreeBuilder is passed")]
        public void Parse_ShouldCreateParseTree_WhenCsvStandardSyntaxTreeBuilderIsPassed()
        {
            // Given
            var input = "one,two\nthree,four";
            var treeVisitor = new CsvStandardSyntaxTreeBuilder();
            var underTest = new CsvCompiler();

            // When
            var node = underTest.Parse(input, treeVisitor) as CsvCompilationUnit;
            var fields = node.ToEnumerable().OfType<FieldSyntax>();
            var allOfThemIsTextField = fields.All(field => field.IsTextField);
            var fieldValues = fields.Select(getTextFromNode);

            // Then
            Assert.That(allOfThemIsTextField, Is.True);
            Assert.That(fieldValues, Is.EquivalentTo(new[]
            {
                "one", "two", "three", "four"
            }));
        }

        #endregion

    }
}
