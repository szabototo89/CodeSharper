using System;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ModifierDescriptor : IHasName, IHasValue<String>, IEquatable<ModifierDescriptor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierDescriptor"/> class.
        /// </summary>
        public ModifierDescriptor(String name, String value, IEnumerable<String> arguments, Type type)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");
            Assume.NotNull(arguments, "arguments");
            Assume.NotNull(type, "type");

            Name = name;
            Value = value;
            Arguments = arguments;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value { get; protected set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        public IEnumerable<String> Arguments { get; protected set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type { get; protected set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(ModifierDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(Name, other.Name) && String.Equals(Value, other.Value) && Equals(Arguments, other.Arguments) && Type == other.Type;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as ModifierDescriptor);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Arguments != null ? Arguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

    }
}