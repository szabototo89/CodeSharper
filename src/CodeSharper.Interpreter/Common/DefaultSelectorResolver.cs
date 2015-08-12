using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.NameMatchers;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;
using CombinatorBase = CodeSharper.Core.Nodes.Combinators.CombinatorBase;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultSelectorResolver : ISelectorResolver
    {
        /// <summary>
        /// Gets or sets the selector manager.
        /// </summary>
        public ISelectorFactory SelectorFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the descriptor repository.
        /// </summary>
        public IDescriptorRepository DescriptorRepository { get; protected set; }

        /// <summary>
        /// Gets or sets the name matcher of resolvers
        /// </summary>
        public INameMatcher NameMatcher { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSelectorResolver"/> class.
        /// </summary>
        public DefaultSelectorResolver(ISelectorFactory selectorFactory, IDescriptorRepository descriptorRepository, INameMatcher nameMatcher = null)
        {
            Assume.NotNull(selectorFactory, nameof(selectorFactory));
            Assume.NotNull(descriptorRepository, nameof(descriptorRepository));

            SelectorFactory = selectorFactory;
            DescriptorRepository = descriptorRepository;
            NameMatcher = nameMatcher ?? new EqualityNameMatcher();
        }

        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        public CombinatorBase Create(SelectorElementBase selectorElement)
        {
            Assume.NotNull(selectorElement, nameof(selectorElement));

            if (selectorElement is UnarySelectorElement)
            {
                var unarySelector = (UnarySelectorElement) selectorElement;
                return create(unarySelector);
            }

            if (selectorElement is BinarySelectorElement)
            {
                var binarySelector = (BinarySelectorElement) selectorElement;
                return create(binarySelector);
            }

            throw new NotSupportedException(
                $"Not supported selector element type: {selectorElement.GetType().FullName}.");
        }

        /// <summary>
        /// Creates the specified unary selectorElement.
        /// </summary>
        private CombinatorBase create(UnarySelectorElement unarySelectorElement)
        {
            var elementTypeSelector = unarySelectorElement.TypeSelectorElement;

            var selectorDescriptor = ResolveSelectorDescriptor(elementTypeSelector);
            if (selectorDescriptor == null)
                throw new NotSupportedException($"Not supported element type selector: {elementTypeSelector.Name}.");

            var classSelectors = ResolveClassSelectors(elementTypeSelector);
            var selector = SelectorFactory.CreateSelector(selectorDescriptor.Type, classSelectors);
            var attributes = ResolverAttributes(elementTypeSelector);
            var modifiers = ResolveModifiers(elementTypeSelector);

            return new SelectionCombinator(selector, modifiers, attributes);
        }

        protected SelectorDescriptor ResolveSelectorDescriptor(TypeSelectorElement typeSelectorElement)
        {
            return DescriptorRepository.GetSelectorDescriptors()
                                       .SingleOrDefault(s => NameMatcher.Match(s.Value, typeSelectorElement.Name));
        }

        protected IEnumerable<SelectorAttribute> ResolverAttributes(TypeSelectorElement typeSelectorElement)
        {
            return typeSelectorElement.Attributes
                                      .Select(attribute => new SelectorAttribute(attribute.Name, attribute.Value.Value));
        }

        protected IEnumerable<ModifierBase> ResolveModifiers(TypeSelectorElement typeSelectorElement)
        {
            return typeSelectorElement.Modifiers
                                      .Select(resolveModifier);
        }

        /// <summary>
        /// Resolves the class selectors.
        /// </summary>
        protected virtual IEnumerable<Regex> ResolveClassSelectors(TypeSelectorElement selector)
        {
            return selector.ClassSelectors.Select<ClassSelectorElement, Regex>(classSelector => {
                var regularClassSelector = classSelector as RegularExpressionClassSelectorElement;
                if (regularClassSelector != null)
                    return regularClassSelector.Regex;

                var rawClassSelector = classSelector as RawClassSelectorElement;
                if (rawClassSelector != null)
                    return new Regex(rawClassSelector.Name);

                throw new NotSupportedException(String.Format("Not supported class selector element: {0}.", classSelector.GetType().FullName));
            });
        }

        /// <summary>
        /// Resolves the pseudo selector by element
        /// </summary>
        private ModifierBase resolveModifier(ModifierElement element)
        {
            var descriptors = DescriptorRepository.GetModifierDescriptors();
            var elements = descriptors.Where(descriptor => NameMatcher.Match(descriptor.Value, element.Name))
                                      .Where(descriptor => descriptor.Arguments.Count() == element.Arguments.Count())
                                      .Select(pseudo => new
                                      {
                                          pseudo.Type,
                                          Arguments = element.Arguments.Select(createModifierSelectorArgument)
                                      })
                                      .ToArray();

            if (elements.Length > 1)
                throw new Exception($"Ambiguity of modifier selector: {element.Name}");
            if (!elements.Any())
                throw new Exception($"Not found modifier selector: {element.Name}");

            var modifier = elements.Single();
            return SelectorFactory.CreateModifier(modifier.Type, modifier.Arguments);
        }

        private Object createModifierSelectorArgument(ConstantElement argument)
        {
            if (argument.Type == typeof (SelectorElementBase))
                return Create(argument.Value as SelectorElementBase);

            return argument.Value;
        }

        /// <summary>
        /// Creates the specified binary selectorElement.
        /// </summary>
        private CombinatorBase create(BinarySelectorElement binarySelectorElement)
        {
            var left = Create(binarySelectorElement.Left);
            var right = Create(binarySelectorElement.Right);

            var combinator = resolveCombinator(binarySelectorElement.CombinatorElement, left, right);
            return combinator;
        }

        /// <summary>
        /// Resolves the CombinatorElement.
        /// </summary>
        private BinaryCombinator resolveCombinator(CombinatorElementBase combinatorElement, CombinatorBase left, CombinatorBase right)
        {
            var combinators = DescriptorRepository.GetCombinatorDescriptors()
                                                  .Where(combinator => NameMatcher.Match(combinator.Value, combinatorElement.Value))
                                                  .ToArray();

            if (combinators.Count() > 1)
                throw new Exception($"Ambiguity of combinator element: {combinatorElement.Value}");

            if (!combinators.Any())
                throw new Exception($"Combinator is not found: {combinatorElement.Value}");

            return SelectorFactory.CreateCombinator(combinators[0].CombinatorType, left, right);
        }
    }
}