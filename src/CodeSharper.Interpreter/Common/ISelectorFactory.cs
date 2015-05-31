using System;
using System.Collections.Generic;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Interpreter.Common
{
    public interface ISelectorFactory
    {
        /// <summary>
        /// Creates a selector by specified name.
        /// </summary>
        SelectorBase CreateSelector(Type selectorType);

        /// <summary>
        /// Creates a pseudo selector.
        /// </summary>
        NodeModifierBase CreatePseudoSelector(Type pseudoSelectorType, IEnumerable<Object> arguments, SelectorBase selector);

        /// <summary>
        /// Creates the combinator.
        /// </summary>
        BinaryCombinator CreateCombinator(Type combinatorType, CombinatorBase left, CombinatorBase right);
    }
}