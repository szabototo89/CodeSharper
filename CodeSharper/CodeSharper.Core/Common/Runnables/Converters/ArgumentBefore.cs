using System;
using System.Reflection;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentBefore<TArgument, TParameter> : IArgumentBefore, IArgumentBefore<TArgument, TParameter>
    {
        public virtual Boolean IsConvertable(Object parameter)
        {
            return parameter is TArgument;
        }

        public virtual Object Convert<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func)
        {
            return Convert((TArgument)parameter, param => func(param));
        }

        public abstract Object Convert<TFunctionResult>(TArgument parameter, Func<TParameter, TFunctionResult> func);
    }
}