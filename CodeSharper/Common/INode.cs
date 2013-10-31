using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Common
{
    public interface INode
    {
        INode Parent { get; }
        IEnumerable<INode> Children { get; }
        IEnumerable<INode> Siblings { get; }
        TextSpan Span { get; }
        string Text { get; }
    }
}
