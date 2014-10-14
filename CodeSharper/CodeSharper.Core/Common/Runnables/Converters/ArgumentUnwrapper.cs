using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentUnwrapper<TArgument, TParameter> : ArgumentConverter<TArgument, TParameter>
        where TArgument : Argument
    {

    }
}