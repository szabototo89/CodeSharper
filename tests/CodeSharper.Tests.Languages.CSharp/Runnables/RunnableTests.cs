using System.Linq;
using System.Runtime.Serialization;
using CodeSharper.Languages.CSharp.Common;
using CodeSharper.Languages.CSharp.Runnables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Text;
using NUnit.Framework;
using Formatter = Microsoft.CodeAnalysis.Formatting.Formatter;

namespace CodeSharper.Tests.Languages.CSharp.Runnables
{
    [TestFixture]
    internal class RunnableTests
    {
        private SyntaxNode FormatSyntaxNode(SyntaxNode node)
        {
            Workspace customWorkspace = new AdhocWorkspace();
            var options = customWorkspace.Options;
            var formattedResult = Formatter.Format(node, customWorkspace, options);
            return formattedResult;
        }

        [Test(Description = "CreateCommentRunnable should remove node from tree and insert a comment when getting syntax node")]
        public void CreateCommentRunnable_ShouldRemoveNodeFromTreeAndInsertAComment_WhenGettingSyntaxNode()
        {
            // Given
            var source = @"
                public void foo() {
                    var a = 10;
                }
            ";
            var node = SyntaxFactory.ParseSyntaxTree(source).GetRoot();
            var underTest = new CreateCommentRunnable();

            // When
            var result = underTest.Run(node).ToFullString();

            // Then
            Assert.That(result, Is.Not.Null);
        }

        [Test(Description = "CreateCommentRunnable should remove node from tree and insert a comment when getting syntax node")]
        public void CreateCommentRunnable_ShouldRemoveNodeFromTreeAndInsertAComment_WhenTransformingMethodBody()
        {
            // Given
            var source = @"
                public void foo() {
                    var a = 10;
                }
            ";
            var compilationUnit = SyntaxFactory.ParseSyntaxTree(source).GetRoot() as CompilationUnitSyntax;
            var variableDeclaration = compilationUnit.DescendantNodes()
                                                     .OfType<MethodDeclarationSyntax>()
                                                     .Single(m => m.Identifier.ValueText == "foo")
                                                     .DescendantNodes()
                                                     .OfType<VariableDeclarationSyntax>()
                                                     .Single();

            var underTest = new CreateCommentRunnable();

            // When
            var result = underTest.Run(variableDeclaration).ToFullString();

            // Then
            Assert.That(result, Is.Not.Null);
        }

        [Test(Description = nameof(ChangeMemberVisibilityRunnable) + " should change node visibility when specific node is passed")]
        public void ChangeMemberVisibility_ShouldChangeVisibilityOfNode_WhenSpecificNodeIsPassed()
        {
            // Given
            var source = @"
                public void foo() {
                    var a = 10;
                }
            ";

            var underTest = new ChangeMemberVisibilityRunnable
            {
                Visibility = "private"
            };

            var workspace = new AdhocWorkspace();
            var project = workspace.AddProject("test", "C#");
            var document = workspace.AddDocument(project.Id, "test", SourceText.From(source));
            var documentContext = new DocumentContext(workspace, document);
            var compilationUnit = document.GetSyntaxRootAsync().Result as CompilationUnitSyntax;
            var memberDeclaration = compilationUnit.DescendantNodes()
                                                   .OfType<MethodDeclarationSyntax>()
                                                   .Single(m => m.Identifier.ValueText == "foo");

            // When
            var result = underTest.Run(memberDeclaration, documentContext).ToFullString();

            // Then
            Assert.That(result, Is.Not.Null);
        }
    }
}