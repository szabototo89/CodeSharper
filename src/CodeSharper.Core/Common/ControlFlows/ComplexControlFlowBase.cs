using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ComplexControlFlowBase : ControlFlowBase, IHasChildren<ControlFlowBase>
    {
        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowBase> Children { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineControlFlow"/> class.
        /// </summary>
        protected ComplexControlFlowBase(IEnumerable<ControlFlowBase> children, IExecutor executor)
            : base(executor)
        {
            Assume.NotNull(children, "children");
            Children = children;
        }
    }
}