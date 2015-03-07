using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Operands
{
    public abstract class NodeSelectorOperandBase
    {
        public abstract IEnumerable<Node> Calculate(IEnumerable<Node> nodes);
    }
}
