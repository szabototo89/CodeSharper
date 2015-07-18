using System;
using System.Collections.Generic;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public interface ISelectorFactory
    {
        /// <summary>
        /// Creates the unary selector element.
        /// </summary>
        UnarySelectorElement CreateUnarySelector(ElementTypeSelector elementTypeSelector);

        /// <summary>
        /// Creates the binary selector element.
        /// </summary>
        BinarySelectorElement CreateBinarySelector(SelectorElementBase left, SelectorElementBase right, CombinatorElementBase @operator);

        /// <summary>
        /// Creates the selector element CombinatorElement.
        /// </summary>
        CombinatorElementBase CreateCombinator(String combinator);

        /// <summary>
        /// Creates a selector element ElementTypeSelector
        /// </summary>
        AttributeElement CreateAttributeSelector(String name, ConstantElement value);

        /// <summary>
        /// Creates a pseudo selector element
        /// </summary>
        PseudoSelectorElement CreatePseudoSelector(String name, IEnumerable<ConstantElement> values);

        /// <summary>
        /// Creates a pseudo selector element
        /// </summary>
        PseudoSelectorElement CreatePseudoSelector(String name, IEnumerable<SelectorElementBase> selectors);

        /// <summary>
        /// Creates a selectable element type selector
        /// </summary>
        ElementTypeSelector CreateElementTypeSelector(String name, IEnumerable<AttributeElement> attributes, IEnumerable<PseudoSelectorElement> pseudoSelectors);

        /// <summary>
        /// Creates a class element selector.
        /// </summary>
        ClassElementSelector CreateClassElementSelector(String name, Boolean isRegularExpression);
    }
}