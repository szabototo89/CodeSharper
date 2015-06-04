using System;

namespace CodeSharper.Core.Common.Runnables
{
    public interface ICastingHelper<out TCast>
    {
        /// <summary>
        /// Casts the specified element.
        /// </summary>
        TCast Cast(Object element);
    }
}