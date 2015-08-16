using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.CSharp.Common;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Languages.CSharp.Runnables
{
    public class CopyToClipboardRunnable : RunnableWithContextBase<IEnumerable<Object>, IEnumerable<SyntaxNode>>
    {
        /// <summary>
        ///     Runs an algorithm with the specified parameter and context
        /// </summary>
        public override IEnumerable<SyntaxNode> Run(IEnumerable<Object> nodes, Object context)
        {
            var documentContext = context.As<CompilationContext>();
            if (documentContext == null)
                throw new Exception($"{nameof(CompilationContext)} is not available.");

            var syntaxNodes = nodes.OfType<SyntaxNode>();
            var sourceTexts = syntaxNodes.GetOrEmpty().Select(node => node.NormalizeWhitespace().ToFullString());
            var textToClipboard = String.Join(Environment.NewLine, sourceTexts);

            var thread = new Thread(() => { Clipboard.SetText(textToClipboard); });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return syntaxNodes;
        }
    }
}