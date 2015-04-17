using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class PipelineControlFlowDescriptor : ControlFlowDescriptorBase, IHasChildren<ControlFlowDescriptorBase>, IEquatable<PipelineControlFlowDescriptor>
    {
        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowDescriptorBase> Children { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineControlFlowDescriptor"/> class.
        /// </summary>
        public PipelineControlFlowDescriptor(IEnumerable<ControlFlowDescriptorBase> children)
            : base(ControlFlowOperationType.Pipeline)
        {
            Assume.NotNull(children, "children");
            Children = children;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(PipelineControlFlowDescriptor other)
        {
            return Equals(other as ControlFlowDescriptorBase) &&
                   Children.SequenceEqual(other.Children);
        }
    }
}