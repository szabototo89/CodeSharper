using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Utilities
{
    public static class ListHelper
    {
        public static List<T> AddItem<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        } 

        public static List<T> AddRange<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
            return list;
        } 
    }
}
