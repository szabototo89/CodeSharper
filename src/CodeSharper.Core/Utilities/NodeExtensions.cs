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
    }
}
