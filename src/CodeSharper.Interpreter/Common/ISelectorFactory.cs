using System;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Interpreter.Common
{
    public interface ISelectorFactory
    {
        /// <summary>
        /// Creates a selector by specified name.
        /// </summary>
        NodeSelectorBase CreateSelector(Type selectorType);

        /// <summary>
        /// Creates a pseudo selector.
        /// </summary>
        NodeModifierBase CreatePseudoSelector(PseudoSelectorElement pseudoSelector, NodeSelectorBase selector);
    }
}