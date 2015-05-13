using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Interpreter.Common
{
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSelectorFactory"/> class.
        /// </summary>
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
        public virtual NodeSelectorBase CreateSelector(Type selectorType)
        {
            Assume.NotNull(selectorType, "selectorType");

            // get default constructor and instantiate it
            var constructors = selectorType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray()) as NodeSelectorBase;

            throw new Exception(String.Format("Cannot find default constructor for selector: {0}", selectorType.Name));
        }

        public BinaryCombinator CreateCombinator(Type combinatorType, CombinatorBase left, CombinatorBase right)
        {
            Assume.NotNull(combinatorType, "combinatorType");
            Assume.NotNull(left, "left");
            Assume.NotNull(right, "right");

            var constructors = combinatorType.GetConstructors();

            // get constructor with two parameters to instantiate the object
            var constructor = constructors.FirstOrDefault(ctor => {
                var parameters = ctor.GetParameters();
                return parameters.Length == 2 &&
                       parameters.Select(param => param.ParameterType)
                                 .All(type => typeof(CombinatorBase).IsAssignableFrom(type));
            });

            if (constructor == null)
                throw new Exception(String.Format("Cannot find Combinator constructor with two CombinatorBase type: {0}", combinatorType.FullName));

            return constructor.Invoke(new Object[] { left, right }) as BinaryCombinator;
        }

        /// <summary>
        /// Creates a pseudo selector.
        /// </summary>
        public virtual NodeModifierBase CreatePseudoSelector(PseudoSelectorElement pseudoSelector, NodeSelectorBase selector)
        {
            Assume.NotNull(selector, "selector");

            var pseudoSelectorType = PseudoSelectors.FirstOrDefault(pseudo => NameMatcher.Match(pseudo.Name, pseudoSelector.Name));
            if (pseudoSelectorType == null)
                throw new Exception(String.Format("{0} pseudo selector is not found.", pseudoSelector.Name));

            // get default constructor and instantiate it
            var constructors = pseudoSelectorType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(new Object[0]) as NodeModifierBase;

            throw new Exception(String.Format("Cannot find default constructor for pseudo selector: {0}", pseudoSelector.Name));
        }
    }
}