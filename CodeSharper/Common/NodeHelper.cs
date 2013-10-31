using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Common
{
    public static class NodeHelper
    {
        public static IEnumerable<TNodeType> GetNodesByType<TNodeType>(this INode node)
            where TNodeType : INode
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node.Children
                        .OfType<TNodeType>()
                        .Concat(node.Children.SelectMany(child => child.GetNodesByType<TNodeType>()));
        }
    }
}
