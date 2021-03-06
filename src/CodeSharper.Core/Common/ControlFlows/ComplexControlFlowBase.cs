using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ComplexControlFlowBase : ControlFlowBase, IHasChildren<ControlFlowBase>
    {
        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowBase> Children { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineControlFlow"/> class.
        /// </summary>
        protected ComplexControlFlowBase(IEnumerable<ControlFlowBase> children)
        {
            Assume.NotNull(children, nameof(children));
            Children = children;
        }
    }
}