using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common.Values
{
    /// <summary>
    /// Represents value of arguments
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class ValueArgument<TValue> : Argument, IEquatable<ValueArgument<TValue>>
    {
        /// <summary>
        /// Default constructor of ValueArgument
        /// </summary>
        /// <param name="value">value of argument</param>
        public ValueArgument(TValue value)
        {
            Value = value;
        }

        /// <summary>
        /// Value of argument object
        /// </summary>
        public TValue Value { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Argument);
        }

        public bool Equals(ValueArgument<TValue> other)
        {
            if (other == null) return false;

            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }
    }
}
