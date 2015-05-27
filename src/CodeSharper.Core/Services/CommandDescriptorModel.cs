using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;

namespace CodeSharper.Core.Services
{
    [DataContract]
    public struct CommandDescriptorModel : IHasName, IEquatable<CommandDescriptorModel>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the command names.
        /// </summary>
        [DataMember(Name = "command-names", IsRequired = false)]
        public IEnumerable<String> CommandNames { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        [DataMember(Name = "arguments", IsRequired = false)]
        public IEnumerable<ArgumentDescriptorModel> Arguments { get; set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(CommandDescriptorModel other)
        {
            return String.Equals(Name, other.Name) && String.Equals(Description, other.Description) && Equals(CommandNames, other.CommandNames) && Equals(Arguments, other.Arguments);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CommandDescriptorModel && Equals((CommandDescriptorModel) obj);
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
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CommandNames != null ? CommandNames.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Arguments != null ? Arguments.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}