using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public abstract class CombinatorBase
    {
        public abstract IEnumerable<Node> Calculate(IEnumerable<Node> nodes);
    }
}
