using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Utilities
{
    public static class EqualityHelper
    {
        /// <summary>
        /// Determines whether the specified element is null or same with other
        /// </summary>
        public static Boolean? IsNullOrReferenceEqual<TElement>(TElement other, TElement @this)
            where TElement : class // to avoid boxing-unboxing
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, @this)) return true;

            return null;
        }

        /// <summary>
        /// Determines whether the specified element is null or same with other
        /// </summary>
        public static Boolean? IsNullOrReferenceEqual<TElement>(TElement other, TElement @this,
            Boolean checkTypeEquality) where TElement : class
        {
            return IsNullOrReferenceEqual(other, @this) ??
                  (checkTypeEquality && other.GetType() == @this.GetType());
        }
    }
}
