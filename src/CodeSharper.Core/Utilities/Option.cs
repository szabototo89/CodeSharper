using CodeSharper.Core.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Utilities
{
    public static class Option
    {
        /// <summary>
        /// Somes the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public static Option<TValue> Some<TValue>(TValue value)
        {
            return new Option<TValue>(value);
        }

        /// <summary>
        /// The none of option
        /// </summary>
        public static readonly None None = None.Instance;
    }

    public struct Option<TValue> : IEquatable<Option<TValue>>
    {
        private TValue value;

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public Boolean HasValue { get; private set; }

        /// <summary>
        /// Gets the value of option
        /// </summary>
        public TValue Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException();

                return value;
            }
            private set { this.value = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Option{TValue}"/> struct.
        /// </summary>
        public Option(TValue value)
            : this()
        {
            Value = value;
            HasValue = true;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="None"/> to <see cref="Option{TValue}"/>.
        /// </summary>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Option<TValue>(None value)
        {
            return new Option<TValue>();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Option{TValue}"/> to <see cref="TValue"/>.
        /// </summary>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator TValue(Option<TValue> value)
        {
            if (!value.HasValue)
                return default(TValue);

            return value.Value;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Boolean operator ==(Option<TValue> left, Option<TValue> right)
        {
            if (left.HasValue != right.HasValue)
                return false;

            if (!left.HasValue)
                return true;

            return Equals(left.Value, right.Value);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Boolean operator !=(Option<TValue> left, Option<TValue> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Indicates whether the current Object is equal to another Object of the same type.
        /// </summary>
        /// <param name="other">An Object to compare with this Object.</param>
        /// <returns>
        /// true if the current Object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(Option<TValue> other)
        {
            if (HasValue == false && other.HasValue == false)
                return true;

            return HasValue.Equals(other.HasValue) && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        /// <summary>
        /// Indicates whether this instance and a specified Object are equal.
        /// </summary>
        /// <param name="obj">Another Object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Option<TValue> && Equals((Option<TValue>) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                if (HasValue == false) return HasValue.GetHashCode()*397;
                return EqualityComparer<TValue>.Default.GetHashCode(Value);
            }
        }

        public Option<TResult> Map<TResult>(Func<TValue, TResult> transform)
        {
            if (!HasValue)
                return Option.None;

            return Option.Some(transform(this.Value));
        }

        public Option<TValue> Filter(Predicate<TValue> predicate)
        {
            if (HasValue && predicate(Value))
                return this;

            return Option.None;
        }
    }

    public sealed class None
    {
        /// <summary>
        /// The instance of <see cref="None"/>
        /// </summary>
        internal static readonly None Instance = new None();

        /// <summary>
        /// Prevents a default instance of the <see cref="None"/> class from being created.
        /// </summary>
        private None()
        {
        }
    }
}