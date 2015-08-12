using System;
using System.Runtime.InteropServices.WindowsRuntime;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Commands
{
    public class NamedCommandCallActualArgument : ICommandCallActualArgument, IEquatable<NamedCommandCallActualArgument>
    {
        /// <summary>
        /// Gets the value of command call actual parameter
        /// </summary>
        public Object Value { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the actual argument
        /// </summary>
        public String ArgumentName { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedCommandCallActualArgument"/> class.
        /// </summary>
        public NamedCommandCallActualArgument(String argumentName, Object value)
        {
            Assume.NotNull(argumentName, nameof(argumentName));
            
            ArgumentName = argumentName;
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
            return Equals(other as NamedCommandCallActualArgument);
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
                return ((Value != null ? Value.GetHashCode() : 0) * 397) ^ (ArgumentName != null ? ArgumentName.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// Indicates whether the current Object is equal to another Object of the same type.
        /// </summary>
        /// <param name="other">An Object to compare with this Object.</param>
        /// <returns>
        /// true if the current Object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Boolean Equals(NamedCommandCallActualArgument other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return Equals(Value, other.Value) && String.Equals(ArgumentName, other.ArgumentName);
        }
    }
}