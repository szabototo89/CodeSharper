using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ValueConsumerBase<TValueIn, TValueOut> : IValueConsumer, IValueConsumer<TValueIn, TValueOut>
    {
        /// <summary>
        /// Determines whether the specified parameter is convertable.
        /// </summary>
        public virtual Boolean IsConvertable(Object parameter)
        {
            return parameter is TValueIn;
        }

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public virtual Object Convert<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func)
        {
            return Convert((TValueIn)parameter, param => func(param));
        }

        /// <summary>
        /// Converts the specified parameter to the proper value
        /// </summary>
        public abstract Object Convert<TFunctionResult>(TValueIn parameter, Func<TValueOut, TFunctionResult> function);
    }
}