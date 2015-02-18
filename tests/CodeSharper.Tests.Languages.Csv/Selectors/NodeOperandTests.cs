using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Selectors.NodeModifiers;
using CodeSharper.Core.Selectors.NodeSelectorOperands;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Selectors;
using CodeSharper.Languages.Csv.SyntaxTrees;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.Csv.Selectors
{
    [TestFixture]
    internal class NodeOperandTests : TestFixtureBase
    {
        [Test(Description = "SelectChildrenOperand should return all children of rows when CsvDocumentNode is passed")]
        public void SelectChildrenOperand_ShouldReturnAllChildrenOfRows_WhenCsvDocumentNodeIsPassed()
        {
            // Given
            var compiler = new CsvCompiler();
            var input = "first,second,third";
            var documentNode = compiler.Parse(input).As<CsvDocumentNode>();
            var underTest = new RowNodeSelector();

            // When
            var left = new SelectAbsoluteNodesOperand(underTest);
            var right = new SelectAbsoluteNodesOperand(new FilterNodeSelector());
            var operand = new SelectChildrenOperand(left, right);

            var result = operand.Calculate(documentNode.Children);

            // Then
            Assert.That(result, Has.All.InstanceOf<FieldNode>());
        }

        [Test(Description = "SelectChildrenOperand should return all children of rows when CsvDocumentNode is passed and SelectRelativeNodesOperand is used")]
        public void SelectChildrenOperand_ShouldReturnAllChildrenOfRows_WhenCsvDocumentNodeIsPassedAndSelectRelativeNodesOperandIsUsed()
        {
            // Given
            var compiler = new CsvCompiler();
            var input = "first,second,third";
            var documentNode = compiler.Parse(input).As<CsvDocumentNode>();
            var underTest = new RowNodeSelector();

            // When
            var left = new SelectRelativeNodesOperand(new RowNodeSelector());
            var right = new SelectAbsoluteNodesOperand(new FilterNodeSelector());
            var operand = new SelectChildrenOperand(left, right);

            var result = operand.Calculate(new[] { documentNode });

            // Then
            Assert.That(result, Has.All.InstanceOf<FieldNode>());
        }

    }
}
