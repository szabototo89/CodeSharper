using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Nodes.Combinators;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ContainsModifier : ModifierBase
    {
        private readonly CombinatorBase combinator;

        /// <summary>
        /// Initializes a new instance of the <see cref="HasModifier"/> class.
        /// </summary>
        public ContainsModifier(Object combinator)
        {
            var rightCombinator = combinator as CombinatorBase;
            this.combinator = new RelativeNodeCombinator(new UniversalCombinator(), rightCombinator);
        }

        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object value)
        {
            if (!(value is IHasChildren<Object>))
                return Enumerable.Empty<Object>();

            var valueWithChildren = (IHasChildren<Object>)value;
            var result = combinator.Calculate(new[] { valueWithChildren });

            if (result.Any())
                return new[] { value };

            return Enumerable.Empty<Object>();
        }
    }
}