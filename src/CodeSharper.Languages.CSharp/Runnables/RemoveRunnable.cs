using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.CSharp.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeSharper.Languages.CSharp.Runnables
{
    [Consumes(typeof(MultiValueConsumer<SyntaxNode>))]
    public class RemoveNodeRunnable : RunnableWithContextBase<SyntaxNode, SyntaxNode>
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

            documentContext.DocumentEditor.RemoveNode(parameter);
            return null;
        }
    }

    public class CopyToClipboardRunnable : RunnableWithContextBase<IEnumerable<Object>, IEnumerable<SyntaxNode>>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter and context
        /// </summary>
        public override IEnumerable<SyntaxNode> Run(IEnumerable<Object> nodes, Object context)
        {
            var documentContext = context.As<DocumentContext>();
            if (documentContext == null)
                throw new Exception("DocumentContext is not available.");

            var syntaxNodes = nodes.OfType<SyntaxNode>();
            var sourceTexts = syntaxNodes.GetOrEmpty().Select(node => node.NormalizeWhitespace().ToFullString());
            var textToClipboard = String.Join(Environment.NewLine, sourceTexts);

            var thread = new Thread(new ThreadStart(() => {
                Clipboard.SetText(textToClipboard);    
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return syntaxNodes;
        }
    }
}