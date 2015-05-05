﻿using System;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Utilities
{
    public static class TypeExtensions
    {
        public static ConstructorInfo GetDefaultConstructor(this Type type)
        {
            Assume.NotNull(type, "type");

            var constructors = type.GetConstructors();
            return constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
        }

        public static TObject InstantiateWithDefaultConstructor<TObject>(this Type value)
        {
            Assume.NotNull(value, "value");

            var defaultConstructor = value.GetDefaultConstructor();
            if (defaultConstructor != null)
                return (TObject)defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray());

            return default(TObject);
        }
    }
}