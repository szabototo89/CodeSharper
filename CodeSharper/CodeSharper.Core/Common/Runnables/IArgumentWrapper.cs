using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IArgumentWrapper
    {
        Boolean IsWrappable(Object parameter);
        
        Boolean IsUnwrappable(Argument parameter);

        Argument Wrap(Object parameter);

        Object Unwrap(Argument argument);
    }
}