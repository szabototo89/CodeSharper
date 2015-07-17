using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Runnables;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeSharper.Languages.CSharp.Runnables
{
    public class RemoveNodeRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<Object>>
    {
        private class RemovalSyntaxRewriter : CSharpSyntaxRewriter
        {
            private readonly List<SyntaxNode> removableNodes;

            /// <summary>
            /// Initializes a new instance of the <see cref="RemovalSyntaxRewriter"/> class.
            /// </summary>
            public RemovalSyntaxRewriter(IEnumerable<SyntaxNode> removableNodes)
            {
                this.removableNodes = new List<SyntaxNode>(removableNodes);
            }

            /// <summary>
            /// Visits the specified node.
            /// </summary>
            public override SyntaxNode Visit(SyntaxNode node)
            {
                if (node == null) return base.Visit(node);

                var children = node.ChildNodes();
                var intersection = children.Intersect(removableNodes).ToArray();
                if (intersection.Any())
                    return node.RemoveNodes(intersection, SyntaxRemoveOptions.KeepNoTrivia);

                return base.Visit(node);
            }
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<Object> Run(IEnumerable<Object> parameter)
        {
            var nodes = parameter.OfType<SyntaxNode>();
            var rewriter = new RemovalSyntaxRewriter(nodes);
            var newRoot = rewriter.Visit(nodes.First().SyntaxTree.GetRoot());
            return new[] {newRoot};
        }
    }
}