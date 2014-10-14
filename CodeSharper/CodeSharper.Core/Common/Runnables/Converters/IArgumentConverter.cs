using System;

namespace CodeSharper.Core.Common.Runnables.Converters
{
    public interface IArgumentConverter
    {
        Boolean IsConvertable(Object parameter);
        
        Object Convert(Object parameter);
    }
}