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
        UnarySelectorElement CreateUnarySelector(TypeSelectorElement typeSelectorElement);

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
        ModifierElement CreateModifier(String name, IEnumerable<ConstantElement> values);

        /// <summary>
        /// Creates a pseudo selector element
        /// </summary>
        ModifierElement CreateModifier(String name, IEnumerable<SelectorElementBase> selectors);

        /// <summary>
        /// Creates a selectable element type selector
        /// </summary>
        TypeSelectorElement CreateElementTypeSelector(String name, IEnumerable<AttributeElement> attributes, IEnumerable<ModifierElement> pseudoSelectors, IEnumerable<ClassSelectorElement> classSelectors);

        /// <summary>
        /// Creates a class element selector.
        /// </summary>
        ClassSelectorElement CreateClassSelectorElement(String name, Boolean isRegularExpression);
    }
}