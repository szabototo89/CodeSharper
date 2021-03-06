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
            Assume.NotNull(combinator, nameof(combinator));

            return new CombinatorElement(combinator);
        }

        /// <summary>
        /// Creates a selectorElement ElementTypeSelector
        /// </summary>
        public AttributeElement CreateAttributeSelector(String name, ConstantElement value)
        {
            Assume.NotNull(name, nameof(name));

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
            Assume.NotNull(name, nameof(name));
            return new ModifierElement(name, values);
        }

        /// <summary>
        /// Creates a pseudo selector element
        /// </summary>
        public ModifierElement CreateModifier(String name, IEnumerable<SelectorElementBase> selectors)
        {
            Assume.NotNull(name, nameof(name));

            var arguments = selectors.Select(selector => new ConstantElement(selector, typeof (SelectorElementBase)));
            return new ModifierElement(name, arguments);
        }

        /// <summary>
        /// Creates a selectable ElementTypeSelector
        /// </summary>
        public TypeSelectorElement CreateElementTypeSelector(String name, IEnumerable<AttributeElement> attributes, IEnumerable<ModifierElement> modifierSelectors, IEnumerable<ClassSelectorElement> classSelectors)
        {
            Assume.NotNull(name, nameof(name));
            Assume.NotNull(attributes, nameof(attributes));
            Assume.NotNull(modifierSelectors, nameof(modifierSelectors));
            Assume.NotNull(classSelectors, nameof(classSelectors));

            return new TypeSelectorElement(name, attributes, modifierSelectors, classSelectors);
        }

        /// <summary>
        /// Creates a class element selector.
        /// </summary>
        public ClassSelectorElement CreateClassSelectorElement(String name, Boolean isRegularExpression)
        {
            Assume.NotNull(name, nameof(name));

            if (isRegularExpression)
                return new RegularExpressionClassSelectorElement(name);

            return new RawClassSelectorElement(name);
        }
    }
}