using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class SequenceControlFlowElement : ControlFlowElementBase, IHasChildren<ControlFlowElementBase>, IEquatable<SequenceControlFlowElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceControlFlowElement"/> class.
        /// </summary>
        public SequenceControlFlowElement(IEnumerable<ControlFlowElementBase> children) 
            : base(ControlFlowOperationType.Sequence)
        {
            Assume.NotNull(children, nameof(children));
            Children = children;
        }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowElementBase> Children { get; protected set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(SequenceControlFlowElement other)
        {
            return Equals(other as ControlFlowElementBase) &&
                   Children.SequenceEqual(other.Children);
        }
    }
}