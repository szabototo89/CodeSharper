using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CodeSharper.Core.Common;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Utilities
{
    public static class NodeExtensions
    {
        public static IEnumerable<Node> ToEnumerable(this Node root)
        {
            return root.Children.Union(root.Children.SelectMany(ToEnumerable));
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
