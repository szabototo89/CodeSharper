using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Utilities
{
    public static class NodeExtensions
    {
        public static IEnumerable<Object> ToEnumerable(this IHasChildren<Object> root)
        {
            return root.Children.Union(
                root.Children
                    .OfType<IHasChildren<Object>>()
                    .SelectMany(ToEnumerable));
        }

        public static void AttachChildren<TNode>(this Node node, IEnumerable<TNode> children)
            where TNode : Node
        {
            foreach (var child in children)
            {
                node.AppendChild(child);
            }
        }

        public static void DetachChildren<TNode>(this Node node, IEnumerable<TNode> children)
            where TNode : Node
        {
            foreach (var child in children.Where(ch => ch != null))
            {
                child.Detach();
            }
        }
    }
}
