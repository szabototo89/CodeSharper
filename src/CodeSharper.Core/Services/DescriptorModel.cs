using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeSharper.Core.Services
{
    [DataContract]
    public struct DescriptorModel : IEquatable<DescriptorModel>
    {
        /// <summary>
        /// Gets or sets the selection descriptors.
        /// </summary>
        [DataMember(Name ="selections", IsRequired = false)]
        public IEnumerable<SelectionDescriptorModel> SelectionDescriptors { get; set; }

        /// <summary>
        /// Gets or sets the command descriptors.
        /// </summary>
        [DataMember(Name = "commands", IsRequired = false)]
        public IEnumerable<CommandDescriptorModel> CommandDescriptors { get; set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(DescriptorModel other)
        {
            return Equals(SelectionDescriptors, other.SelectionDescriptors) && Equals(CommandDescriptors, other.CommandDescriptors);
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
            if (ReferenceEquals(null, obj)) return false;
            return obj is DescriptorModel && Equals((DescriptorModel) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((SelectionDescriptors != null ? SelectionDescriptors.GetHashCode() : 0)*397) ^ (CommandDescriptors != null ? CommandDescriptors.GetHashCode() : 0);
            }
        }

        #endregion

    }
}