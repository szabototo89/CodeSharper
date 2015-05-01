using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public abstract class CombinatorBase
    {
        /// <summary>
        /// Calculates the specified nodes.
        /// </summary>
        public abstract IEnumerable<Node> Calculate(IEnumerable<Node> nodes);
    }
}
