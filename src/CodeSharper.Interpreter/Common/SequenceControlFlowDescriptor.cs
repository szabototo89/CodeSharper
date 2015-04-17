using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class SequenceControlFlowDescriptor : ControlFlowDescriptorBase, IHasChildren<ControlFlowDescriptorBase>, IEquatable<SequenceControlFlowDescriptor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceControlFlowDescriptor"/> class.
        /// </summary>
        public SequenceControlFlowDescriptor(IEnumerable<ControlFlowDescriptorBase> children) 
            : base(ControlFlowOperationType.Sequence)
        {
            Assume.NotNull(children, "children");
            Children = children;
        }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowDescriptorBase> Children { get; protected set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(SequenceControlFlowDescriptor other)
        {
            return Equals(other as ControlFlowDescriptorBase) &&
                   Children.SequenceEqual(other.Children);
        }
    }
}