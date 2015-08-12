using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public abstract class CombinatorElementBase : IHasValue<String>, IEquatable<CombinatorElementBase>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorElementBase"/> class.
        /// </summary>
        protected CombinatorElementBase(String value)
        {
            Assume.NotNull(value, nameof(value));
            Value = value;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(CombinatorElementBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(Value, other.Value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as CombinatorElementBase);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        #endregion
    }
}