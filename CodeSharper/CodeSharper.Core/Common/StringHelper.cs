using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common
{
    public static class StringHelper
    {
        private static String _TransformWords(this String text, Func<IEnumerable<String>, IEnumerable<String>> transform)
        {
            Constraints.NotNull(() => transform);

            const string separator = " ";
            return String.Join(separator, transform(text.Split(new[] { separator }, StringSplitOptions.None)));           
        }

        public static String TakeWords(this String text, Int32 count)
        {
            return _TransformWords(text, words => words.Take(count));
        }

        public static String SkipWords(this String text, Int32 count)
        {
            return _TransformWords(text, words => words.Skip(count));
        }

    }
}