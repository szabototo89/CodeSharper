using System;

namespace CodeSharper.Core.Texts
{
    static internal class StringHelper
    {
        public static String ReplaceByStartAndLength(this String oldValue, Int32 start, Int32 length, String newValue)
        {
            return oldValue.Remove(start, length)
                .Insert(start, newValue);
        }
    }
}