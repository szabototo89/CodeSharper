using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common.Values
{
    public static class Arguments
    {
        public static ValueArgument<T> Value<T>(T value)
        {
            return new ValueArgument<T>(value);
        }

        public static ErrorArgument Error(String message = "")
        {
            return new ErrorArgument(message);
        }

        public static TypeErrorArgument<T> TypeError<T>(String message = "")
        {
            throw new System.NotImplementedException();
        }

        public static MultiValueArgument<T> MultiValue<T>(IEnumerable<T> values)
        {
            return new MultiValueArgument<T>(values);
        }

        public static IEnumerable<ValueArgument<T>> Values<T>(IEnumerable<T> values)
        {
            Constraints.NotNull(() => values);

            return values.Select(value => Value(value));
        }
    }
}