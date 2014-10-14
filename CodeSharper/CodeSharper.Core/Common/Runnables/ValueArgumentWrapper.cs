using System.Reflection;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public class ValueArgumentWrapper<TParameter> 
        : ArgumentWrapper<TParameter, ValueArgument<TParameter>>
    {
        protected override ValueArgument<TParameter> Convert(TParameter parameter)
        {
            return Arguments.Value(parameter);
        }
    }
}