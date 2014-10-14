using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public abstract class ArgumentWrapper<TParameter, TArgument> : ArgumentConverter<TParameter, TArgument>
        where TArgument : Argument
    {
        
    }
}