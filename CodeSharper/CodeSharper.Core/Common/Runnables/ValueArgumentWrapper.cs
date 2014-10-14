using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public class ValueArgumentWrapper<TParameter> : ArgumentWrapper<TParameter, ValueArgument<TParameter>>
    {
        protected override ValueArgument<TParameter> Wrap(TParameter parameter)
        {
            return Arguments.Value(parameter);
        }

        protected override TParameter Unwrap(ValueArgument<TParameter> argument)
        {
            return argument.Value;
        }
    }
}