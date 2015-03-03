using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Utilities
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TValue> GetOrEmpty<TValue>(this IEnumerable<TValue> enumerable)
        {
            return enumerable ?? Enumerable.Empty<TValue>();
        } 

        public static IEnumerable<TResult> Select<TElement, TResult>(this TElement element, Func<TElement, TResult> func)
            where TElement : IHasNext<TElement>
        {
            Assume.NotNull(func, "func");

            var current = element;

            while (current != null)
            {
                yield return func(current);
                current = current.Next;
            }
        }

        public static IEnumerable<TElement> AsEnumerable<TElement>(this TElement element)
            where TElement : IHasNext<TElement>
        {
            var current = element;

            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
    }
}
