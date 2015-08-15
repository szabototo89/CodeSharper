using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Commands
{
    public class PositionedCommandCallActualArgument : ICommandCallActualArgument, IEquatable<PositionedCommandCallActualArgument>
    {
        /// <summary>
        /// Gets the value of command call actual parameter
        /// </summary>
        public Object Value { get; }

        /// <summary>
        /// Gets or sets the position of actual argument
        /// </summary>
        public Int32 Position { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionedCommandCallActualArgument"/> class.
        /// </summary>
        public PositionedCommandCallActualArgument(Int32 position, Object value)
        {
            Assume.IsRequired(position >= 0, "position must be positive or zero!");

            Position = position;
            Value = value;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified Object  is equal to the current Object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object other)
        {
            return Equals(other as PositionedCommandCallActualArgument);
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
                return ((Value != null ? Value.GetHashCode() : 0)*397) ^ Position;
            }
        }

        /// <summary>
        /// Indicates whether the current Object is equal to another Object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current Object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An Object to compare with this Object.</param>
        public Boolean Equals(PositionedCommandCallActualArgument other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return Equals(Value, other.Value) && Position == other.Position;
        }
    }
}