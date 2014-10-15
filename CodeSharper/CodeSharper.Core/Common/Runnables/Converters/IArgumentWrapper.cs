using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IArgumentWrapper
    {
        Boolean IsWrappable(Object parameter);

        Object Wrap(Object parameter);
    }
}