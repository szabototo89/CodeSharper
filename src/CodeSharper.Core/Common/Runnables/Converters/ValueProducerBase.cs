using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ValueProducerBase<TValueIn, TValueOut> : IValueProducer, IValueProducer<TValueIn, TValueOut>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public virtual Boolean IsConvertable(Object parameter)
        {
            return parameter is TValueIn;
        }

        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        public virtual Object Convert(Object parameter)
        {
            return Convert((TValueIn)parameter);
        }

        /// <summary>
        /// Converts the specified parameter to the proper format
        /// </summary>
        public abstract TValueOut Convert(TValueIn parameter);
    }
}