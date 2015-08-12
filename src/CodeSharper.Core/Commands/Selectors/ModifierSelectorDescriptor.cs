using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands.Selectors
{
    public class ModifierSelectorDescriptor : IHasName, IHasValue<Option<Object>>, 
                                            IEquatable<ModifierSelectorDescriptor>
    {
        /// <summary>
        /// Gets or sets the name of <see cref="ModifierSelectorDescriptor"/> class
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets or sets the value of <see cref="ModifierSelectorDescriptor"/> class
        /// </summary>
        public Option<Object> Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierSelectorDescriptor"/> class.
        /// </summary>
        public ModifierSelectorDescriptor(String name, Option<Object> value)
        {
            Assume.NotNull(name, nameof(name));

            Name = name;
            Value = value;
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
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Value.GetHashCode();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified Object  is equal to the current Object; otherwise, false.
        /// </returns>
        /// <param name="other">The Object to compare with the current Object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object other)
        {
            return Equals(other as ModifierSelectorDescriptor);
        }

        /// <summary>
        /// Indicates whether the current Object is equal to another Object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current Object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An Object to compare with this Object.</param>
        public Boolean Equals(ModifierSelectorDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(Name, other.Name) &&
                   Value.Equals(other.Value);
        }
    }
}