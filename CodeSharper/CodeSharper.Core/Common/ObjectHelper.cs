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

        public static Boolean Is<T>(this Object that)
        {
            return that is T;
        }

        public static T As<T>(this Object that) where T : class
        {
            return that as T;
        }

        public static T To<T>(this Object that)
        {
            return (T)that;
        }

        public static IEnumerable<T> Repeat<T>(this T value, Int32 count)
        {
            return Enumerable.Repeat(value, count);
        } 
    }
}
