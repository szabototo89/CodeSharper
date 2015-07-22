using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        SelectorBase CreateSelector(Type selectorType, IEnumerable<Regex> classSelectors);

        /// <summary>
        /// Creates a modifier.
        /// </summary>
        ModifierBase CreateModifier(Type modifierType, IEnumerable<Object> arguments);

        /// <summary>
        /// Creates the combinator.
        /// </summary>
        BinaryCombinator CreateCombinator(Type combinatorType, CombinatorBase left, CombinatorBase right);

        /// <summary>
        /// Creates a class selector.
        /// </summary>
        ModifierBase CreateClassSelector(Type classSelectorType, String className);
    }
}