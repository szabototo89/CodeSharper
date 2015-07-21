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
        /// Creates a selector by specified name.
        /// </summary>
        public virtual SelectorBase CreateSelector(Type selectorType)
        {
            Assume.NotNull(selectorType, "selectorType");

            // get default constructor and instantiate it
            var constructors = selectorType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray()) as SelectorBase;

            throw new Exception(String.Format("Cannot find default constructor for selector: {0}", selectorType.FullName));
        }

        /// <summary>
        /// Creates the combinator.
        /// </summary>
        public virtual BinaryCombinator CreateCombinator(Type combinatorType, CombinatorBase left, CombinatorBase right)
        {
            Assume.NotNull(combinatorType, "combinatorType");
            Assume.NotNull(left, "left");
            Assume.NotNull(right, "right");

            var constructors = combinatorType.GetConstructors();

            // get constructor with two CombinatorBase parameters to instantiate the object
            var constructor = constructors.FirstOrDefault(ctor => {
                var parameters = ctor.GetParameters();
                return parameters.Length == 2 &&
                       parameters.Select(param => param.ParameterType)
                                 .All(type => typeof (CombinatorBase).IsAssignableFrom(type));
            });

            if (constructor == null)
                throw new Exception(String.Format("Cannot find Combinator constructor with two CombinatorBase type: {0}", combinatorType.FullName));

            return constructor.Invoke(new Object[] {left, right}) as BinaryCombinator;
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