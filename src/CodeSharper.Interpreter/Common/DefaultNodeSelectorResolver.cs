using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Utilities;
using CombinatorBase = CodeSharper.Core.Nodes.Combinators.CombinatorBase;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultNodeSelectorResolver : INodeSelectorResolver
    {
        private readonly Dictionary<Type, Func<CombinatorBase, CombinatorBase, BinaryCombinator>> _registeredCombinators;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNodeSelectorResolver"/> class.
        /// </summary>
        public DefaultNodeSelectorResolver()
        {
            _registeredCombinators = new Dictionary<Type, Func<CombinatorBase, CombinatorBase, BinaryCombinator>>();
            _registeredCombinators.Add(typeof(ChildCombinatorElement), (left, right) => new ChildrenCombinator(left, right));
            _registeredCombinators.Add(typeof(DescendantCombinatorElement), (left, right) => new RelativeNodeCombinator(left, right));
        }

        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        public CombinatorBase Create(SelectorElementBase selectorElement)
        {
            Assume.NotNull(selectorElement, "selectorElement");

            if (selectorElement is UnarySelectorElement)
            {
                var unarySelector = (UnarySelectorElement)selectorElement;
                return create(unarySelector);
            }

            if (selectorElement is BinarySelectorElement)
            {
                var binarySelector = (BinarySelectorElement)selectorElement;
                return create(binarySelector);
            }

            throw new NotSupportedException("selectorElement is not supported.");
        }

        /// <summary>
        /// Creates the specified unary selectorElement.
        /// </summary>
        private CombinatorBase create(UnarySelectorElement unarySelectorElement)
        {
            var elementTypeSelector = unarySelectorElement.ElementTypeSelector;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified binary selectorElement.
        /// </summary>
        private CombinatorBase create(BinarySelectorElement binarySelectorElement)
        {
            var left = Create(binarySelectorElement.Left);
            var right = Create(binarySelectorElement.Right);

            var combinator = resolveCombinator(binarySelectorElement.CombinatorElement, left, right);
            return combinator;
        }

        /// <summary>
        /// Resolves the CombinatorElement.
        /// </summary>
        private BinaryCombinator resolveCombinator(CombinatorElementBase combinatorElement, CombinatorBase left, CombinatorBase right)
        {
            var registeredCombinator = _registeredCombinators.TryGetValue(combinatorElement.GetType());
            if (!registeredCombinator.HasValue)
                throw new NotSupportedException(String.Format("Not supported CombinatorElement: {0}", combinatorElement.Value));

            var factory = registeredCombinator.Value;
            return factory(left, right);
        }
    }
}