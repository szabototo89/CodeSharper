using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Text;

namespace CodeSharper.Languages.CSharp.Runnables
{
    public class CreateRunnable : RunnableBase<Object, SyntaxNode>
    {
        [Parameter("source")]
        public String Source { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override SyntaxNode Run(Object parameter)
        {
            return CSharpSyntaxTree.ParseText(Source).GetRoot();
        }
    }

    [Consumes(typeof(MultiValueConsumer<SyntaxNode>))]
    public class CreateCommentRunnable : RunnableBase<SyntaxNode, SyntaxNode>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override SyntaxNode Run(SyntaxNode parameter)
        {
            if (parameter == null) return null;

            var sourceText = parameter.WithoutTrivia().ToFullString();
            var commentedSourceText = String.Format("/* {0} */", sourceText);
            AdhocWorkspace workspace = new AdhocWorkspace();
            var document = workspace.CurrentSolution.GetDocument(parameter.SyntaxTree);
            var editor = DocumentEditor.CreateAsync(document).Result;

            // handle root element
            if (parameter.Parent == null)
                return parameter.SyntaxTree.WithChangedText(SourceText.From(commentedSourceText)).GetRoot();

            // if it is not root element then insert after it and remove the original one
            var comment = SyntaxFactory.Comment(commentedSourceText);
            // var commentedNode = SyntaxFactory.MissingToken(SyntaxKind.EmptyStatement).WithLeadingTrivia(comment);

            return parameter.WithLeadingTrivia(comment);
        }
    }
}