using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public class DefaultNodeSelectorFactory : INodeSelectorFactory
    {
        /// <summary>
        /// Creates the unary selectorElement.
        /// </summary>
        public UnarySelectorElement CreateUnarySelector(ElementTypeSelector elementTypeSelector)
        {
            return new UnarySelectorElement(elementTypeSelector);
        }

        /// <summary>
        /// Creates the binary selectorElement.
        /// </summary>
        public BinarySelectorElement CreateBinarySelector(SelectorElementBase left, SelectorElementBase right, CombinatorElementBase selectorOperator)
        {
            return new BinarySelectorElement(left, right, selectorOperator);
        }

        /// <summary>
        /// Creates the selectorElement CombinatorElement.
        /// </summary>
        public CombinatorElementBase CreateCombinator(String combinator)
        {
            Assume.NotNull(combinator, "CombinatorElement");

            return new CombinatorElement(combinator);
        }

        /// <summary>
        /// Creates a selectorElement ElementTypeSelector
        /// </summary>
        public AttributeElement CreateAttributeSelector(String name, ConstantElement value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new AttributeElement {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a pseudo selectorElement
        /// </summary>
        public PseudoSelectorElement CreatePseudoSelector(String name, IEnumerable<ConstantElement> values)
        {
            Assume.NotNull(name, "name");

            return new PseudoSelectorElement {
                Name = name,
                Arguments = values
            };
        }

        /// <summary>
        /// Creates a selectable ElementTypeSelector
        /// </summary>
        public ElementTypeSelector CreateElementTypeSelector(String name, IEnumerable<AttributeElement> attributes, IEnumerable<PseudoSelectorElement> pseudoSelectors)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(attributes, "attributes");
            Assume.NotNull(pseudoSelectors, "pseudoSelectors");

            return new ElementTypeSelector {
                Name = name,
                Attributes = attributes,
                PseudoSelectors = pseudoSelectors
            };
        }
    }
}