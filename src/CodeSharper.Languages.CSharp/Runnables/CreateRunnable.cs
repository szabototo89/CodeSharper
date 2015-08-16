using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

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
}