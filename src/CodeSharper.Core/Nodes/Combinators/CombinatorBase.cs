using System;
using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public abstract class CombinatorBase
    {
        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public abstract IEnumerable<Object> Calculate(IEnumerable<Object> values);
    }
}
