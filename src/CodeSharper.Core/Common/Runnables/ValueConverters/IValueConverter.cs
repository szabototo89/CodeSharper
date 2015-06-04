using System;

namespace CodeSharper.Core.Common.Runnables.ValueConverters
{
    public interface IValueConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified value.
        /// </summary>
        Boolean CanConvert(Object value);

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        Object Convert(Object value);
    }
}