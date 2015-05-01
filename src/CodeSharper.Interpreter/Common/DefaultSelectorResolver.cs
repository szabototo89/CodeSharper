using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
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
        /// Initializes a new instance of the <see cref="DefaultSelectorResolver"/> class.
        /// </summary>
        public DefaultSelectorResolver(ISelectorFactory selectorFactory)
        {
            Assume.NotNull(selectorFactory, "SelectorFactory");

            _registeredCombinators = new Dictionary<Type, Func<CombinatorBase, CombinatorBase, BinaryCombinator>>();
            _registeredCombinators.Add(typeof(ChildCombinatorElement), (left, right) => new ChildrenCombinator(left, right));
            _registeredCombinators.Add(typeof(DescendantCombinatorElement), (left, right) => new RelativeNodeCombinator(left, right));

            SelectorFactory = selectorFactory;
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

            throw new NotSupportedException("selectorElement is not supported.");
        }

        /// <summary>
        /// Creates the specified unary selectorElement.
        /// </summary>
        private CombinatorBase create(UnarySelectorElement unarySelectorElement)
        {
            var elementTypeSelector = unarySelectorElement.ElementTypeSelector;

            var selector = SelectorFactory.CreateSelector(elementTypeSelector.Name);
            var pseudoSelectors = elementTypeSelector.PseudoSelectors.Select(pseudo => SelectorFactory.CreatePseudoSelector(selector, pseudo));

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

    public interface ISelectorFactory
    {
        /// <summary>
        /// Creates a selector by specified name.
        /// </summary>
        NodeSelectorBase CreateSelector(String name);

        /// <summary>
        /// Creates a pseudo selector.
        /// </summary>
        NodeModifierBase CreatePseudoSelector(NodeSelectorBase selector, PseudoSelectorElement pseudoSelector);
    }

    public class DefaultSelectorFactory : ISelectorFactory
    {
        /// <summary>
        /// Gets or sets the selectors.
        /// </summary>
        public IEnumerable<Type> Selectors { get; protected set; }

        /// <summary>
        /// Gets or sets the pseudo selectors.
        /// </summary>
        public IEnumerable<Type> PseudoSelectors { get; protected set; }

        /// <summary>
        /// Gets or sets the name matcher.
        /// </summary>
        public INameMatcher NameMatcher { get; protected set; }

        public DefaultSelectorFactory(IEnumerable<Type> selectors, IEnumerable<Type> pseudoSelectors, INameMatcher nameMatcher = null)
        {
            Assume.NotNull(selectors, "selectors");
            Assume.NotNull(pseudoSelectors, "pseudoSelectors");

            Selectors = selectors;
            PseudoSelectors = pseudoSelectors;
            NameMatcher = nameMatcher ?? new EqualityNameMatcher();
        }

        /// <summary>
        /// Creates a selector by specified name.
        /// </summary>
        public virtual NodeSelectorBase CreateSelector(String name)
        {
            Assume.NotBlank(name, "name");
            
            // resolve selector by name
            var selectorType = Selectors.FirstOrDefault(type => NameMatcher.Match(type.Name, name));
            if (selectorType == null)
                throw new Exception(String.Format("{0} selector is not found.", name));

            // get default constructor
            var constructors = selectorType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray()) as NodeSelectorBase;

            throw new Exception(String.Format("Cannot find default constructor for selector: {0}", name));
        }

        /// <summary>
        /// Creates a pseudo selector.
        /// </summary>
        public virtual NodeModifierBase CreatePseudoSelector(NodeSelectorBase selector, PseudoSelectorElement pseudoSelector)
        {
            throw new NotImplementedException();
        }
    }
}