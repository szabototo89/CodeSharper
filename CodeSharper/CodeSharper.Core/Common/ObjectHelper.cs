using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common
{
    public static class ObjectHelper
    {
        public static T[] AsArray<T>(this T that)
        {
            return new[] { that };
        }

        public static List<T> AsList<T>(this T that)
        {
            return new List<T>(new[] { that });
        }
    }
}
