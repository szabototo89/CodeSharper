﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common.Values
{
    /// <summary>
    /// Represents value of arguments
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class ValueArgument<TValue> : Argument, IValueArgument, IEquatable<ValueArgument<TValue>>
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

        public override Boolean Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return Equals(obj as ValueArgument<TValue>);
        }

        public Boolean Equals(ValueArgument<TValue> other)
        {
            if (other == null) return false;

            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override Int32 GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }

        Object IValueArgument.Value { get { return Value; } }
    }
}
