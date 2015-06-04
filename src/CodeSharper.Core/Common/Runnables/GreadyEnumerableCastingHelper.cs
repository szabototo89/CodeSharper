using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables
{
    public class GreadyEnumerableCastingHelper<TCast> : EnumerableCastingHelper<TCast>
    {
        /// <summary>
        /// Casts the specified element.
        /// </summary>
        public override IEnumerable<TCast> Cast(Object element)
        {
            return base.Cast(element).ToArray();
        }
    }
}