using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors.NodeSelectorOperands
{
    public abstract class NodeSelectorOperandBase
    {
        public abstract IEnumerable<Node> Calculate(IEnumerable<Node> nodes);
    }
}
