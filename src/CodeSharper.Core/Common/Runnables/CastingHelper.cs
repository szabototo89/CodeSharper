using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public interface ICastingHelper<out TCast>
    {
        /// <summary>
        /// Casts the specified element.
        /// </summary>
        TCast Cast(Object element);
    }

    public class SimpleCastingHelper<TCast> : ICastingHelper<TCast>
    {
        /// <summary>
        /// Casts the specified element.
        /// </summary>
        public TCast Cast(Object element)
        {
            return (TCast)element;
        }
    }

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