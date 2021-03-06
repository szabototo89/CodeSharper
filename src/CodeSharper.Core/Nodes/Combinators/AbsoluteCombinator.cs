﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    [Obsolete("Use ChildrenCombinator instead of this")]
    public class AbsoluteCombinator : CombinatorBase
    {
        /// <summary>
        /// Gets or sets the node selector
        /// </summary>
        public SelectorBase Selector { get; }

        /// <summary>
        /// Gets the node modifiers.
        /// </summary>
        public IEnumerable<ModifierBase> NodeModifiers { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbsoluteCombinator"/> class.
        /// </summary>
        public AbsoluteCombinator(SelectorBase selector, IEnumerable<ModifierBase> nodeModifiers = null)
        {
            Assume.NotNull(selector, nameof(selector));

            Selector = selector;
            NodeModifiers = nodeModifiers.GetOrEmpty();
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var filteredNodes = values.SelectMany(node => Selector.SelectElement(node));

            if (NodeModifiers.Any())
            {
                foreach (var node in filteredNodes)
                {
                    foreach (var modifier in NodeModifiers)
                    {
                        var modifiedNodes = modifier.ModifySelection(node);
                        foreach (var modifiedNode in modifiedNodes)
                        {
                            yield return modifiedNode;
                        }
                    }
                }
            }
            else
            {
                foreach (var filteredNode in filteredNodes)
                {
                    yield return filteredNode;
                }       
            }
        }
    }
}
