using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Utilities
{
    public static class EqualityHelper
    {
        public static Boolean? IsNullOrReferenceEqual<TElement>(TElement other, TElement @this)
            where TElement : class // to avoid boxing-unboxing
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, @this)) return true;

            return null;
        }
    }
}
