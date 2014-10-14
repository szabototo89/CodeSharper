using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public class ValueArgumentUnwrapper<TParameter>
        : ArgumentUnwrapper<ValueArgument<TParameter>, TParameter>
    {
        protected override TParameter Convert(ValueArgument<TParameter> parameter)
        {
            return parameter.Value;
        }
    }
}