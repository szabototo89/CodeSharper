using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public interface INodeSelectorFactory
    {
        /// <summary>
        /// Creates the unary selector.
        /// </summary>
        UnarySelector CreateUnarySelector(ElementTypeSelector elementTypeSelector);

        /// <summary>
        /// Creates the binary selector.
        /// </summary>
        BinarySelector CreateBinarySelector(BaseSelector left, BaseSelector right, CombinatorBase @operator);

        /// <summary>
        /// Creates the selector combinator.
        /// </summary>
        CombinatorBase CreateCombinator(String combinator);

        /// <summary>
        /// Creates a selector ElementTypeSelector
        /// </summary>
        AttributeSelector CreateAttributeSelector(String name, Constant value);

        /// <summary>
        /// Creates a pseudo selector
        /// </summary>
        PseudoSelector CreatePseudoSelector(String name, Constant value);

        /// <summary>
        /// Creates a selectable ElementTypeSelector
        /// </summary>
        ElementTypeSelector CreateElementTypeSelector(String name, IEnumerable<AttributeSelector> attributes, IEnumerable<PseudoSelector> pseudoSelectors);
    }

    public class DefaultNodeSelectorFactory : INodeSelectorFactory
    {
        /// <summary>
        /// Creates the unary selector.
        /// </summary>
        public UnarySelector CreateUnarySelector(ElementTypeSelector elementTypeSelector)
        {
            return new UnarySelector(elementTypeSelector);
        }

        /// <summary>
        /// Creates the binary selector.
        /// </summary>
        public BinarySelector CreateBinarySelector(BaseSelector left, BaseSelector right, CombinatorBase selectorOperator)
        {
            return new BinarySelector(left, right, selectorOperator);
        }

        /// <summary>
        /// Creates the selector combinator.
        /// </summary>
        public CombinatorBase CreateCombinator(String combinator)
        {
            Assume.NotNull(combinator, "combinator");

            switch (combinator)
            {
                case ">":
                    return new ChildCombinator();
                case "":
                    return new DescendantCombinator();
                default:
                    throw new NotSupportedException(String.Format("Not supported combinator: {0}.", combinator));
            }
        }

        /// <summary>
        /// Creates a selector ElementTypeSelector
        /// </summary>
        public AttributeSelector CreateAttributeSelector(String name, Constant value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new AttributeSelector {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a pseudo selector
        /// </summary>
        public PseudoSelector CreatePseudoSelector(String name, Constant value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new PseudoSelector {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a selectable ElementTypeSelector
        /// </summary>
        public ElementTypeSelector CreateElementTypeSelector(String name, IEnumerable<AttributeSelector> attributes, IEnumerable<PseudoSelector> pseudoSelectors)
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