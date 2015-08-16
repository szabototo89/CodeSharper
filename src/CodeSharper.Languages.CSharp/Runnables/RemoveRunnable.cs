using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Languages.CSharp.Common;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Languages.CSharp.Runnables
{
    [Consumes(typeof (MultiValueConsumer<SyntaxNode>))]
    public class RemoveNodeRunnable : RunnableWithContextBase<SyntaxNode, SyntaxNode, CompilationContext>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override SyntaxNode Run(SyntaxNode parameter, CompilationContext context)
        {
            if (parameter == null) return null;
            Assume.IsRequired(context, nameof(context));

            context.DocumentEditor.RemoveNode(parameter);
            return null;
        }
    }
}