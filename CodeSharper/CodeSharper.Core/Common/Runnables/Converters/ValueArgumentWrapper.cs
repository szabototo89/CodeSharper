using System.Reflection;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ValueArgumentWrapper<TParameter> 
        : ArgumentWrapper<TParameter, ValueArgument<TParameter>>
    {
        public override ValueArgument<TParameter> Wrap(TParameter parameter)
        {
            return Arguments.Value(parameter);
        }
    }
}