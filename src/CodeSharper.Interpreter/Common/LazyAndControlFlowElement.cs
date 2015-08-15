using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class LazyAndControlFlowElement : ControlFlowElementBase, IHasChildren<ControlFlowElementBase>, IEquatable<LazyAndControlFlowElement>
    {
        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowElementBase> Children { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyAndControlFlowElement"/> class.
        /// </summary>
        public LazyAndControlFlowElement(IEnumerable<ControlFlowElementBase> children) 
            : base(ControlFlowOperationType.LazyAnd)
        {
            Assume.NotNull(children, nameof(children));   
            Children = children;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(LazyAndControlFlowElement other)
        {
            return base.Equals(other) &&
                   Children.SequenceEqual(other.Children);
        }
    }
}