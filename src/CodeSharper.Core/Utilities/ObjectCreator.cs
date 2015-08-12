using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Utilities
{
    /// <summary>
    /// Helper class for creating object dynamically
    /// </summary>
    public class ObjectCreator
    {
        /// <summary>
        /// Creates a new object by the specified object type and constructor arguments.
        /// </summary>
        public Object Create(Type objectType, params Object[] constructorArguments)
        {
            Assume.NotNull(objectType, nameof(objectType));

            if (objectType.IsValueType)
                throw new NotSupportedException($"Value types are not supported: {objectType.FullName}.");

            var constructors = objectType.GetConstructors();
            var constructor = constructors.FirstOrDefault(ctor => MatchConstructorWithArguments(ctor, constructorArguments));
            if (constructor == null) return null;

            return constructor.Invoke(constructorArguments);
        }

        private Boolean MatchConstructorWithArguments(ConstructorInfo ctor, Object[] constructorArguments)
        {
            var arguments = ctor.GetParameters();
            if (arguments.Length != constructorArguments.Length)
                return false;

            // dealing with default constructor
            if (constructorArguments.Length == 0) return true;

            for (var i = 0; i < arguments.Length; i++)
            {
                var constructorArgument = constructorArguments[i];
                var constructorArgumentType = constructorArgument?.GetType() ?? typeof (Object);
                var parameterType = arguments[i].ParameterType;

                if (parameterType != constructorArgumentType &&
                    !parameterType.IsAssignableFrom(constructorArgumentType))
                    return false;
            }

            return true;
        }
    }
}