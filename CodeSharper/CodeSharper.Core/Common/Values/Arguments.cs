using System;
using System.Collections.Generic;
using System.Linq;

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

        public static MultiValueArgument MultiValue<T>(IEnumerable<T> source)
        {
            return new MultiValueArgument(source.Select(Value));
        }
    }
}