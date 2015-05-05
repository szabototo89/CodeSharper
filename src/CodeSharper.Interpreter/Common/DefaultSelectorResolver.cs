using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;
using CombinatorBase = CodeSharper.Core.Nodes.Combinators.CombinatorBase;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultSelectorResolver : ISelectorResolver
    {
        private readonly Dictionary<Type, Func<CombinatorBase, CombinatorBase, BinaryCombinator>> _registeredCombinators;

        /// <summary>
        /// Gets or sets the selector manager.
        /// </summary>
        public ISelectorFactory SelectorFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the descriptor repository.
        /// </summary>
        public IDescriptorRepository DescriptorRepository { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSelectorResolver"/> class.
        /// </summary>
        public DefaultSelectorResolver(ISelectorFactory selectorFactory, IDescriptorRepository descriptorRepository)
        {
            Assume.NotNull(selectorFactory, "selectorFactory");
            Assume.NotNull(descriptorRepository, "descriptorRepository");

            _registeredCombinators = new Dictionary<Type, Func<CombinatorBase, CombinatorBase, BinaryCombinator>>();
            _registeredCombinators.Add(typeof(ChildCombinatorElement), (left, right) => new ChildrenCombinator(left, right));
            _registeredCombinators.Add(typeof(DescendantCombinatorElement), (left, right) => new RelativeNodeCombinator(left, right));

            SelectorFactory = selectorFactory;
            DescriptorRepository = descriptorRepository;
        }

        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        public CombinatorBase Create(SelectorElementBase selectorElement)
        {
            Assume.NotNull(selectorElement, "selectorElement");

            if (selectorElement is UnarySelectorElement)
            {
                var unarySelector = (UnarySelectorElement)selectorElement;
                return create(unarySelector);
            }

            if (selectorElement is BinarySelectorElement)
            {
                var binarySelector = (BinarySelectorElement)selectorElement;
                return create(binarySelector);
            }

            throw new NotSupportedException(String.Format("Selector element is not supported: {0}.", selectorElement.GetType().FullName));
        }

        /// <summary>
        /// Creates the specified unary selectorElement.
        /// </summary>
        private CombinatorBase create(UnarySelectorElement unarySelectorElement)
        {
            var elementTypeSelector = unarySelectorElement.ElementTypeSelector;

            var selectorDescriptor = DescriptorRepository.LoadSelectors().SingleOrDefault(s => s.Value == elementTypeSelector.Name);
            if (selectorDescriptor == null)
                throw new NotSupportedException(String.Format("Not supported element type selector: {0}.", elementTypeSelector.Name));

            var selector = SelectorFactory.CreateSelector(selectorDescriptor.Type);
            var pseudoSelectors = elementTypeSelector.PseudoSelectors.Select(pseudo => SelectorFactory.CreatePseudoSelector(pseudo, selector));

            return new NodeSelectionCombinator(selector, pseudoSelectors);
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
            var registeredCombinator = _registeredCombinators.TryGetValue(combinatorElement.GetType());
            if (!registeredCombinator.HasValue)
                throw new NotSupportedException(String.Format("Not supported CombinatorElement: {0}", combinatorElement.Value));

            var factory = registeredCombinator.Value;
            return factory(left, right);
        }
    }
}