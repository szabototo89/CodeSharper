using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentConverter<TInParameter, TOutParameter> : IArgumentConverter
    {
        public virtual Boolean IsConvertable(Object parameter)
        {
            return parameter is TInParameter;
        }

        public virtual Object Convert(Object parameter)
        {
            return Convert((TInParameter)parameter);
        }

        protected abstract TOutParameter Convert(TInParameter parameter);
    }
}