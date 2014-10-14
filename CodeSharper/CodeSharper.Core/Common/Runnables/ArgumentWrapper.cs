using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public abstract class ArgumentWrapper<TParameter, TArgument> : ArgumentConverter<TParameter, TArgument>
        where TArgument : Argument
    {
        
    }

    public abstract class ArgumentUnwrapper<TArgument, TParameter> : ArgumentConverter<TArgument, TParameter>
        where TArgument : Argument
    {

    }

}