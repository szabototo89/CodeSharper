using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Utilities;
using CombinatorBase=CodeSharper.Core.Nodes.Combinators.CombinatorBase;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultNodeSelectorResolver : INodeSelectorResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNodeSelectorResolver"/> class.
        /// </summary>
        public DefaultNodeSelectorResolver()
        {
        
        }

        /// <summary>
        /// Creates the specified selector.
        /// </summary>
        public Core.Nodes.Combinators.CombinatorBase Create(BaseSelector selector)
        {
            Assume.NotNull(selector, "selector");

            if (selector is UnarySelector)
            {
                var unarySelector = (UnarySelector)selector;
                return create(unarySelector);
            }

            if (selector is BinarySelector)
            {
                var binarySelector = (BinarySelector) selector;
                return create(binarySelector);
            }

            throw new NotSupportedException("Selector is not supported.");
        }

        /// <summary>
        /// Creates the specified unary selector.
        /// </summary>
        private Core.Nodes.Combinators.CombinatorBase create(UnarySelector unarySelector)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified binary selector.
        /// </summary>
        private Core.Nodes.Combinators.CombinatorBase create(BinarySelector binarySelector)
        {
            var left = Create(binarySelector.Left);
            var right = Create(binarySelector.Right);

            var combinator = resolveCombinator(binarySelector.Combinator, left, right);

            return combinator;
        }

        /// <summary>
        /// Resolves the combinator.
        /// </summary>
        private Core.Nodes.Combinators.CombinatorBase resolveCombinator(CombinatorBase combinator, Core.Nodes.Combinators.CombinatorBase left, Core.Nodes.Combinators.CombinatorBase right)
        {
            if (combinator is ChildCombinator)
            {
                return new ChildrenCombinator(left, new AbsoluteCombinator());
            }
        }
    }
}