using System;
using System.Runtime.CompilerServices;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables.ValueConverters
{
    public class IntegerValueConverter : IValueConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified value.
        /// </summary>
        public Boolean CanConvert(Object value, Type conversionType)
        {
            return value.IsNumber();
        }

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        public Object Convert(Object value)
        {
            if (value is Int32)
            {
                return value;
            }

            return System.Convert.ToInt32(value);
        }
    }
}