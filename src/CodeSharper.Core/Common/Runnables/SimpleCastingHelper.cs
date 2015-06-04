using System;
using System.Collections;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public class SimpleCastingHelper<TCast> : ICastingHelper<TCast>
    {
        /// <summary>
        /// Casts the specified element.
        /// </summary>
        public TCast Cast(Object element)
        {
            return (TCast) element;
        }
    }
}