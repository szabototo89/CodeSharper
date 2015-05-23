using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class UniversalCombinator : NodeSelectionCombinator  
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalCombinator"/> class.
        /// </summary>
        public UniversalCombinator(IEnumerable<NodeModifierBase> modifiers = null)
            : base(new UniversalSelector(), modifiers)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            if (values != null && Modifiers.Any())
            {
                var result = new List<Object>();
                foreach (var modifier in Modifiers)
                {
                    result.AddRange(values.SelectMany(node => modifier.ModifySelection(node)));
                }
                return result;
            }

            return values;
        }
    }
}