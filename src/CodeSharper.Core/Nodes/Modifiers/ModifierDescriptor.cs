﻿using System;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ModifierDescriptor : IHasName, IHasValue<String>, IEquatable<ModifierDescriptor>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value { get; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        public IEnumerable<String> Arguments { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is class selector.
        /// </summary>
        public Boolean IsClassSelector { get; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierDescriptor"/> class.
        /// </summary>
        public ModifierDescriptor(String name, String value, IEnumerable<String> arguments, Type type, Boolean isClassSelector)
        {
            Assume.NotNull(name, nameof(name));
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(arguments, nameof(arguments));
            Assume.NotNull(type, nameof(type));

            Name = name;
            Value = value;
            Arguments = arguments;
            Type = type;
            IsClassSelector = isClassSelector;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ModifierDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(Name, other.Name) && String.Equals(Value, other.Value) && Equals(Arguments, other.Arguments) && IsClassSelector == other.IsClassSelector && Equals(Type, other.Type);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ModifierDescriptor) obj);
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
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Arguments != null ? Arguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsClassSelector.GetHashCode();
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}