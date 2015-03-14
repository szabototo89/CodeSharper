using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands.Selectors
{
    public class SelectorDescriptor : IEquatable<SelectorDescriptor>, IHasValue<String>
    {
        /// <summary>
        /// Gets or sets the value of selector
        /// </summary>
        public String Value { get; protected set; }

        /// <summary>
        /// Gets or sets the attributes of selector
        /// </summary>
        public IEnumerable<SelectorAttributeDescriptor> SelectorAttributes { get; protected set; }

        /// <summary>
        /// Gets or sets the pseudo selector descriptors
        /// </summary>
        public IEnumerable<PseudoSelectorDescriptor> PseudoSelectorDescriptors { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorDescriptor"/> class.
        /// </summary>
        public SelectorDescriptor(String value, IEnumerable<SelectorAttributeDescriptor> selectorAttributes, IEnumerable<PseudoSelectorDescriptor> pseudoSelectorDescriptors)
        {
            Assume.NotNull(value, "value");

            Value = value;
            SelectorAttributes = selectorAttributes.GetOrEmpty();
            PseudoSelectorDescriptors = pseudoSelectorDescriptors.GetOrEmpty();
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
                var hashCode = (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SelectorAttributes != null ? SelectorAttributes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PseudoSelectorDescriptors != null ? PseudoSelectorDescriptors.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="other">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object other)
        {
            return Equals(other as SelectorDescriptor);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(SelectorDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(Value, other.Value) &&
                   Enumerable.SequenceEqual(SelectorAttributes, other.SelectorAttributes) &&
                   Enumerable.SequenceEqual(PseudoSelectorDescriptors, other.PseudoSelectorDescriptors);
        }
    }
}
