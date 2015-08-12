using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Languages.CSharp.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

    [Consumes(typeof (MultiValueConsumer<SyntaxNode>))]
    public class CreateCommentRunnable : RunnableWithContextBase<SyntaxNode, SyntaxNode>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override SyntaxNode Run(SyntaxNode parameter, Object context)
        {
            if (parameter == null) return null;
            var documentContext = context as DocumentContext;
            if (documentContext == null)
                throw new Exception("document context is not available.");

            var sourceText = parameter.WithoutTrivia().ToFullString();
            var commentedSourceText = String.Format("/* {0} */", sourceText);
            var editor = documentContext.DocumentEditor;

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