using System;

namespace CodeSharper.Core.Common.Runnables.ValueConverters
{
    public class IntegerValueConverter : IValueConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified value.
        /// </summary>
        public Boolean CanConvert(Object value)
        {
            return value is Single || value is Double;
        }

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        public Object Convert(Object value)
        {
            return System.Convert.ToInt32(value);
        }
    }
}