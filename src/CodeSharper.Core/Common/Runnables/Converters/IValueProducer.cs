using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IValueProducer
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        Boolean IsConvertable(Object parameter);

        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        Object Convert(Object parameter);
    }

    public interface IValueProducer<in TValueIn, out TValueOut>
    {
        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        TValueOut Convert(TValueIn parameter);
    }
}