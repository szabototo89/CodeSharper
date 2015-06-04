using System;

namespace CodeSharper.Core.Common.Runnables.ValueConverters
{
    public class EmptyValueConverter : IValueConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified value.
        /// </summary>
        public Boolean CanConvert(Object value)
        {
            return false;
        }

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        public Object Convert(Object value)
        {
            throw new NotSupportedException();
        }
    }
}