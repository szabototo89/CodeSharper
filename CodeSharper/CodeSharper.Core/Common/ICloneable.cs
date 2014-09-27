using System;

namespace CodeSharper.Core.Common
{
    public interface ICloneable<out T> : ICloneable
    {
        new T Clone();
    }
}