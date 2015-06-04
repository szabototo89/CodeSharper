using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Runnables
{
    public class EnumerableCastingHelper<TCast> : ICastingHelper<IEnumerable<TCast>>
    {
        /// <summary>
        /// Casts the specified element.
        /// </summary>
        public virtual IEnumerable<TCast> Cast(Object element)
        {
            if (element is IEnumerable<TCast>)
            {
                return (IEnumerable<TCast>)element;
            }

            if (element is IEnumerable<Object>)
            {
                var enumerable = (IEnumerable<Object>)element;
                return enumerable.Cast<TCast>();
            }

            throw new Exception(String.Format("Cannot cast to enumerable of {0}", typeof(TCast).FullName));
        }
    }
}