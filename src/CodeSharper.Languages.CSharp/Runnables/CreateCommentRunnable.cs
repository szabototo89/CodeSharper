using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Languages.CSharp.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace CodeSharper.Languages.CSharp.Runnables
{
    [Consumes(typeof (MultiValueConsumer<SyntaxNode>))]
    public class CreateCommentRunnable : RunnableWithContextBase<SyntaxNode, SyntaxNode, CompilationContext>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override SyntaxNode Run(SyntaxNode parameter, CompilationContext context)
        {
            if (parameter == null) return null;
            Assume.IsRequired(context);

            var sourceText = parameter.WithoutTrivia().ToFullString();
            var commentedSourceText = $"/* {sourceText} */";
            var editor = context.DocumentEditor;

            // handle root element
            if (parameter.Parent == null)
            {
                editor.RemoveNode(parameter);
                return parameter.SyntaxTree.WithChangedText(SourceText.From(commentedSourceText)).GetRoot();
            }

            // if it is not root element then insert after it and remove the original one
            var comment = SyntaxFactory.Comment(commentedSourceText);
            var commentedResult = SyntaxFactory.EmptyStatement().WithLeadingTrivia(comment);
            editor.InsertBefore(parameter, commentedResult);

            return commentedResult;
        }
    }
}