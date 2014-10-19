using System;
using System.Reflection;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentUnwrapper<TArgument, TParameter> : IArgumentUnwrapper, IArgumentUnwrapper<TArgument, TParameter>
    {
        public virtual Boolean IsUnwrappable(Object parameter)
        {
            return parameter is TArgument;
        }

        public virtual Object Unwrap<TFunctionResult>(Object parameter, Func<Object, TFunctionResult> func)
        {
            return Unwrap((TArgument)parameter, param => func(param));
        }

        public abstract Object Unwrap<TFunctionResult>(TArgument parameter, Func<TParameter, TFunctionResult> func);
    }
}