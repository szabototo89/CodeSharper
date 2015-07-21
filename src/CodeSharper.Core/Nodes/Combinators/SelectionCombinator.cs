using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class SelectionCombinator : UnaryCombinator
    {
        /// <summary>
        /// Gets or sets the node selector.
        /// </summary>
        public SelectorBase Selector { get; protected set; }

        /// <summary>
        /// Gets or sets the modifiers.
        /// </summary>
        public IEnumerable<ModifierBase> Modifiers { get; protected set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public IEnumerable<SelectorAttribute> Attributes { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionCombinator"/> class.
        /// </summary>
        public SelectionCombinator(SelectorBase selector, IEnumerable<ModifierBase> modifiers = null, IEnumerable<SelectorAttribute> attributes = null)
        {
            Assume.NotNull(selector, "selector");

            Selector = selector;
            Modifiers = modifiers.GetOrEmpty();
            Attributes = attributes.GetOrEmpty();
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            Selector.ApplyAttributes(Attributes);
            var filteredElements = values.GetOrEmpty().SelectMany(node => Selector.SelectElement(node));

            foreach (var filteredElement in filteredElements)
            {
                if (Modifiers.Any())
                {
                    foreach (var modifier in Modifiers)
                    {
                        foreach (var element in modifier.ModifySelection(filteredElement))
                        {
                            yield return element;
                        }
                    }
                }
                else
                {
                    yield return filteredElement;
                }
            }
        }
    }
}