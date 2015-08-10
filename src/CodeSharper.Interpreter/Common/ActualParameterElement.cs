using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Interpreter.Common
{
    public class ActualParameterElement : IHasValue<ConstantElement>, IEquatable<ActualParameterElement>
    {
        /// <summary>
        /// Gets or sets the name of the parameter
        /// </summary>
        public Option<String> ParameterName { get; protected set; }

        /// <summary>
        /// Gets or sets the position of actual parameter
        /// </summary>
        public Option<Int32> Position { get; protected set; }

        /// <summary>
        /// Gets or sets the value of actual parameter
        /// </summary>
        public ConstantElement Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActualParameterElement"/> class.
        /// </summary>
        public ActualParameterElement(ConstantElement value, Int32 position)
        {
            Assume.NotNull(value, nameof(value));

            Value = value;
            Position = Option.Some(position);
            ParameterName = Option.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActualParameterElement"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public ActualParameterElement(ConstantElement value, String parameterName)
        {
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(parameterName, nameof(parameterName));

            Value = value;
            ParameterName = Option.Some(parameterName);
            Position = Option.None;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ActualParameterElement other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   ParameterName.Equals(other.ParameterName) &&
                   Position.Equals(other.Position) &&
                   Equals(Value, other.Value);
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
            return Equals(obj as ActualParameterElement);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = ParameterName.GetHashCode();
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}