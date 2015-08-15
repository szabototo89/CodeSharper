using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Utilities
{
    public static class TypeExtensions
    {
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Type type, Boolean inherit)
            where TAttribute : Attribute
        {
            Assume.IsRequired(type, nameof(type));

            return type.GetCustomAttributes(inherit)
                       .OfType<TAttribute>();
        }

        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            Assume.NotNull(type, nameof(type));

            var constructors = type.GetConstructors();
            return constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
        }

        public static TObject InstantiateWithDefaultConstructor<TObject>(this Type value)
        {
            Assume.NotNull(value, nameof(value));

            var defaultConstructor = value.GetDefaultConstructor();
            if (defaultConstructor != null)
                return (TObject)defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray());

            return default(TObject);
        }
    }
}