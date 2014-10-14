using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
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