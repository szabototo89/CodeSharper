using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IArgumentConverter
    {
        Boolean IsConvertable(Object parameter);
        
        Object Convert(Object parameter);
    }
}