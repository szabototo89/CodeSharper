using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Visitors;

namespace CodeSharper.Interpreter.Common
{
    public class ConstantElement : IHasValue<Object>, IEquatable<ConstantElement>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public Object Value { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantElement"/> class.
        /// </summary>
        public ConstantElement(Object value, Type type)
        {
            Assume.NotNull(type, "type");

            Value = value;
            Type = type;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ConstantElement other)
        {
           return EqualityHelper.IsNullOrReferenceEqual(other, this) ?? 
                  Equals(Value, other.Value) &&
                  Type == other.Type;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as ConstantElement);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((Value != null ? Value.GetHashCode() : 0)*397) ^ (Type != null ? Type.GetHashCode() : 0);
            }
        }

        #endregion
    }
}