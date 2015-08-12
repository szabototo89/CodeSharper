using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Utilities
{
    public class ConstructsHelper
    {
        public static IEnumerable<TValue> Array<TValue>()
        {
            return Enumerable.Empty<TValue>();
        } 

        public static IEnumerable<TValue> Array<TValue>(params TValue[] values)
        {
            return values;
        }
    }
}