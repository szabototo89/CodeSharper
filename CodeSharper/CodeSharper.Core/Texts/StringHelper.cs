using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    static internal class StringHelper
    {
        public static Int32 IndexOf(this String value, IEnumerable<String> values, Int32 startIndex)
        {
            Constraints.NotNull(() => value);
            var result = -1;

            foreach (var searchedValue in values)
            {
                result = value.IndexOf(searchedValue, startIndex, StringComparison.Ordinal);
                if (result != -1)
                    return result;
            }

            return result;
        }

        public static String ReplaceByStartAndLength(this String oldValue, Int32 start, Int32 length, String newValue)
        {
            return oldValue.Remove(start, length)
                .Insert(start, newValue);
        }
    }
}