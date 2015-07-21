using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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
            Assume.NotNull(selectorFactory, "selectorFactory");
            Assume.NotNull(descriptorRepository, "descriptorRepository");

            SelectorFactory = selectorFactory;
            DescriptorRepository = descriptorRepository;
            NameMatcher = nameMatcher ?? new EqualityNameMatcher();
        }

        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        public CombinatorBase Create(SelectorElementBase selectorElement)
        {
            Assume.NotNull(selectorElement, "selectorElement");

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

            throw new NotSupportedException(String.Format("Not supported selector element type: {0}.", selectorElement.GetType().FullName));
        }

        /// <summary>
        /// Creates the specified unary selectorElement.
        /// </summary>
        private CombinatorBase create(UnarySelectorElement unarySelectorElement)
        {
            var elementTypeSelector = unarySelectorElement.ElementTypeSelector;

            var selectorDescriptor = ResolveSelectorDescriptor(elementTypeSelector);
            if (selectorDescriptor == null)
                throw new NotSupportedException(String.Format("Not supported element type selector: {0}.", elementTypeSelector.Name));

            var selector = SelectorFactory.CreateSelector(selectorDescriptor.Type);
            var attributes = ResolverAttributes(elementTypeSelector);
            var modifiers = ResolveModifiers(elementTypeSelector);
            var classSelectors = ResolveClassSelectors(elementTypeSelector);

            return new SelectionCombinator(selector, classSelectors.Concat(modifiers), attributes);
        }

        protected SelectorDescriptor ResolveSelectorDescriptor(ElementTypeSelector elementTypeSelector)
        {
            return DescriptorRepository.GetSelectorDescriptors()
                                       .SingleOrDefault(s => NameMatcher.Match(s.Value, elementTypeSelector.Name));
        }

        protected IEnumerable<SelectorAttribute> ResolverAttributes(ElementTypeSelector elementTypeSelector)
        {
            return elementTypeSelector.Attributes
                                      .Select(attribute => new SelectorAttribute(attribute.Name, attribute.Value.Value));
        }

        protected IEnumerable<ModifierBase> ResolveModifiers(ElementTypeSelector elementTypeSelector)
        {
            return elementTypeSelector.Modifiers
                                      .Select(resolveModifier);
        }

        /// <summary>
        /// Resolves the class selectors.
        /// </summary>
        protected virtual IEnumerable<ModifierBase> ResolveClassSelectors(ElementTypeSelector selector)
        {
            var 

            return selector.ClassSelectors
                           .Select(classSelector => SelectorFactory.CreateClassSelector(classSelector.Name));
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
                                          Arguments = element.Arguments.Select(createPseudoSelectorArgument)
                                      })
                                      .ToArray();

            if (elements.Length > 1)
                throw new Exception(String.Format("Ambiguity of pseudo selector: {0}", element.Name));
            if (!elements.Any())
                throw new Exception(String.Format("Not found pseudo selector: {0}", element.Name));

            var modifier = elements.Single();
            return SelectorFactory.CreateModifier(modifier.Type, modifier.Arguments);
        }

        private Object createPseudoSelectorArgument(ConstantElement argument)
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
                throw new Exception(String.Format("Ambiguity of combinator element: {0}", combinatorElement.Value));

            if (!combinators.Any())
                throw new Exception(String.Format("Combinator is not found: {0}", combinatorElement.Value));

            return SelectorFactory.CreateCombinator(combinators[0].CombinatorType, left, right);
        }
    }
}