using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Nodes;
using CodeSharper.Languages.Csv.Nodes.Selectors;
using CodeSharper.Languages.Csv.SyntaxTrees;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.Csv.Nodes
{
    [TestFixture]
    internal class NodeOperandTests : TestFixtureBase
    {
        [Test(Description = "ChildrenCombinator should return all children of rows when CsvCompilationUnit is passed")]
        public void SelectChildrenOperand_ShouldReturnAllChildrenOfRows_WhenCsvDocumentNodeIsPassed()
        {
            // Given
            var compiler = new CsvCompiler();
            var input = "first,second,third";
            var documentNode = compiler.Parse(input).As<CsvCompilationUnit>();
            var underTest = new RowNodeSelector();

            // When
            var left = new AbsoluteCombinator(underTest);
            var right = new AbsoluteCombinator(new FieldNodeSelector());
            var combinator = new ChildrenCombinator(left, right);

            var result = combinator.Calculate(documentNode.Children);

            // Then
            Assert.That(result, Has.All.InstanceOf<FieldDeclarationSyntax>());
        }

        [Test(Description = "ChildrenCombinator should return all children of rows when CsvCompilationUnit is passed and RelativeCombinator is used")]
        public void SelectChildrenOperand_ShouldReturnAllChildrenOfRows_WhenCsvDocumentNodeIsPassedAndSelectRelativeNodesOperandIsUsed()
        {
            // Given
            var compiler = new CsvCompiler();
            var input = "first,second,third";
            var documentNode = compiler.Parse(input).As<CsvCompilationUnit>();

            // When
            var left = new RelativeCombinator(new RowNodeSelector());
            var right = new AbsoluteCombinator(new FieldNodeSelector());
            var combinator = new ChildrenCombinator(left, right);

            var result = combinator.Calculate(new[] { documentNode });

            // Then
            Assert.That(result, Has.All.InstanceOf<FieldDeclarationSyntax>());
        }

    }
}
