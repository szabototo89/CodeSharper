using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class HasNodeModifier : NodeModifierBase
    {
        private readonly CombinatorBase combinator;

        /// <summary>
        /// Initializes a new instance of the <see cref="HasNodeModifier"/> class.
        /// </summary>
        public HasNodeModifier(Object combinator)
        {
            this.combinator = combinator as CombinatorBase;
        }

        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object value)
        {
            if (!(value is IHasChildren<Object>))
                return Enumerable.Empty<Object>();

            var valueWithChildren = (IHasChildren<Object>) value;
            var result = combinator.Calculate(valueWithChildren.Children);

            if (result.Any())
                return new[] { value };

            return Enumerable.Empty<Object>();
        }
    }
}