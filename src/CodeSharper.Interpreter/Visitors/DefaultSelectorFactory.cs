using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public class DefaultSelectorFactory : ISelectorFactory
    {
        /// <summary>
        /// Creates the unary selectorElement.
        /// </summary>
        public UnarySelectorElement CreateUnarySelector(TypeSelectorElement typeSelectorElement)
        {
            return new UnarySelectorElement(typeSelectorElement);
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

            return new AttributeElement {
                Name = name,
                Value = value ?? new ConstantElement(null, typeof(Object))
            };
        }

        /// <summary>
        /// Creates a pseudo selectorElement
        /// </summary>
        public ModifierElement CreateModifier(String name, IEnumerable<ConstantElement> values)
        {
            Assume.NotNull(name, "name");

            return new ModifierElement {
                Name = name,
                Arguments = values
            };
        }

        /// <summary>
        /// Creates a pseudo selector element
        /// </summary>
        public ModifierElement CreateModifier(String name, IEnumerable<SelectorElementBase> selectors)
        {
            Assume.NotNull(name, "name");

            return new ModifierElement {
                Name = name,
                Arguments = selectors.Select(selector => new ConstantElement(selector, typeof(SelectorElementBase)))
            };
        }

        /// <summary>
        /// Creates a selectable ElementTypeSelector
        /// </summary>
        public TypeSelectorElement CreateElementTypeSelector(String name, IEnumerable<AttributeElement> attributes, IEnumerable<ModifierElement> pseudoSelectors, IEnumerable<ClassSelectorElement> classSelectors)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(attributes, "attributes");
            Assume.NotNull(pseudoSelectors, "pseudoSelectors");
            Assume.NotNull(classSelectors, "classSelectors");

            return new TypeSelectorElement {
                Name = name,
                Attributes = attributes,
                Modifiers = pseudoSelectors,
                ClassSelectors = classSelectors
            };
        }

        /// <summary>
        /// Creates a class element selector.
        /// </summary>
        public ClassSelectorElement CreateClassSelectorElement(String name, Boolean isRegularExpression)
        {
            Assume.NotNull(name, "name");

            if (isRegularExpression)
                return new RegularExpressionClassSelectorElement(name);

            return new RawClassSelectorElement(name);
        }
    }
}