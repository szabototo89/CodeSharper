using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Values
{
    public class MultiValueArgument<TValue> : Argument, IValueArgument, IMultiValueArgument, IEquatable<MultiValueArgument<TValue>>
    {
        public MultiValueArgument(IEnumerable<TValue> values)
        {
            Values = values ?? Enumerable.Empty<TValue>();
        }

        public IEnumerable<TValue> Values { get; protected set; }

        public Boolean Equals(MultiValueArgument<TValue> other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Values, other.Values);
        }

        IEnumerable<Object> IMultiValueArgument.Values { get { return Values.OfType<Object>(); }}
        
        Object IValueArgument.Value { get { return Values; } }
    }
}