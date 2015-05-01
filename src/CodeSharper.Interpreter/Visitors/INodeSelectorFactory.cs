using System;
using System.Collections.Generic;
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
}