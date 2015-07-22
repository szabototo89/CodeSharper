using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Interpreter.Common
{
    public class DefaultSelectorFactory : ISelectorFactory
    {
        private readonly ObjectCreator objectCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSelectorFactory"/> class.
        /// </summary>
        public DefaultSelectorFactory()
        {
            objectCreator = new ObjectCreator();
        }

        /// <summary>
        /// Creates a selector by specified name.
        /// </summary>
        public virtual SelectorBase CreateSelector(Type selectorType, IEnumerable<Regex> classSelectors)
        {
            Assume.NotNull(selectorType, "selectorType");

            var selector = (SelectorBase)objectCreator.Create(selectorType);
            if (selector == null)
                throw new Exception(String.Format("Cannot instantiate specified selector: {0}", selectorType.FullName));

            selector.AddClassSelectors(classSelectors);

            return selector;
        }

        /// <summary>
        /// Creates the combinator.
        /// </summary>
        public virtual BinaryCombinator CreateCombinator(Type combinatorType, CombinatorBase left, CombinatorBase right)
        {
            Assume.NotNull(combinatorType, "combinatorType");
            Assume.NotNull(left, "left");
            Assume.NotNull(right, "right");

            /*var constructors = combinatorType.GetConstructors();

            // get constructor with two CombinatorBase parameters to instantiate the object
            var constructor = constructors.FirstOrDefault(ctor => {
                var parameters = ctor.GetParameters();
                return parameters.Length == 2 &&
                       parameters.Select(param => param.ParameterType)
                                 .All(type => typeof (CombinatorBase).IsAssignableFrom(type));
            });

            if (constructor == null)
                throw new Exception(String.Format("Cannot find Combinator constructor with two CombinatorBase type: {0}", combinatorType.FullName));

            return constructor.Invoke(new Object[] {left, right}) as BinaryCombinator;*/
            
            var combinator = (BinaryCombinator)objectCreator.Create(combinatorType, left, right);
            if (combinator == null)
                throw new Exception(String.Format("Cannot find Combinator constructor with two CombinatorBase type: {0}", combinatorType.FullName));

            return combinator;
        }

        /// <summary>
        /// Creates a modifier.
        /// </summary>
        public virtual ModifierBase CreateModifier(Type modifierType, IEnumerable<Object> arguments)
        {
            Assume.NotNull(modifierType, "pseudoSelectorType");
            Assume.NotNull(arguments, "arguments");

            var args = arguments.ToArray();

            // get default constructor and instantiate it
            var constructors = modifierType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == args.Length);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(args) as ModifierBase;

            throw new Exception(String.Format("Cannot find default constructor for pseudo selector: {0}", modifierType.FullName));
        }

        /// <summary>
        /// Creates a class selector.
        /// </summary>
        public virtual ModifierBase CreateClassSelector(Type classSelectorType, String className)
        {
            Assume.NotNull(classSelectorType, "classSelectorType");
            Assume.NotBlank(className, "className");

            var constructors = classSelectorType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(
                constructor => {
                    var args = constructor.GetParameters().ToArray();
                    return args.Length == 1 && args.First().ParameterType == typeof (String);
                });

            if (defaultConstructor == null)
                throw new Exception("ClassSelector type needs to have one-parameter constructor.");

            return defaultConstructor.Invoke(new[] {className}) as ModifierBase;
        }
    }
}