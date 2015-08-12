using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
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
        public IEnumerable<ModifierSelectorDescriptor> PseudoSelectorDescriptors { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorDescriptor"/> class.
        /// </summary>
        public SelectorDescriptor(String value, IEnumerable<SelectorAttributeDescriptor> selectorAttributes, IEnumerable<ModifierSelectorDescriptor> pseudoSelectorDescriptors)
        {
            Assume.NotNull(value, nameof(value));

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
        /// true if the specified Object  is equal to the current Object; otherwise, false.
        /// </returns>
        /// <param name="other">The Object to compare with the current Object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object other)
        {
            return Equals(other as SelectorDescriptor);
        }

        /// <summary>
        /// Indicates whether the current Object is equal to another Object of the same type.
        /// </summary>
        /// <param name="other">An Object to compare with this Object.</param>
        /// <returns>
        /// true if the current Object is equal to the <paramref name="other" /> parameter; otherwise, false.
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
