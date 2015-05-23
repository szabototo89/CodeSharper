using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Services
{
    [DataContract]
    public struct SelectionDescriptorModel : IEquatable<SelectionDescriptorModel>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the selector.
        /// </summary>
        [DataMember(Name = "selector-type", IsRequired = false)]
        public String SelectorType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DataMember(Name = "value", IsRequired = false)]
        public String Value { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type", IsRequired = false)]
        public String Type { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        [DataMember(Name = "arguments", IsRequired = false)]
        public IEnumerable<String> Arguments { get; set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(SelectionDescriptorModel other)
        {
            return String.Equals(Name, other.Name) &&
                   String.Equals(SelectorType, other.SelectorType) &&
                   String.Equals(Value, other.Value) &&
                   String.Equals(Type, other.Type);
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
            if (obj is SelectionDescriptorModel)
                return Equals((SelectionDescriptorModel)obj);

            return false;
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
                hashCode = (hashCode * 397) ^ (SelectorType != null ? SelectorType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
