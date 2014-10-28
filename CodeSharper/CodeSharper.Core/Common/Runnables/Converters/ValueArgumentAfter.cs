using System.Reflection;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ValueArgumentAfter<TParameter> 
        : ArgumentAfter<TParameter, ValueArgument<TParameter>>
    {
        public override ValueArgument<TParameter> Convert(TParameter parameter)
        {
            return Arguments.Value(parameter);
        }
    }
}