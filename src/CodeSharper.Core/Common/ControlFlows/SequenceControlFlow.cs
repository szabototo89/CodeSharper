using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Common.ControlFlows
{
    public class SequenceControlFlow : ComplexControlFlowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceControlFlow"/> class.
        /// </summary>
        public SequenceControlFlow(IEnumerable<ControlFlowBase> children) : base(children)
        {

        }

        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public override Object Execute(Object parameter)
        {
            var result = parameter;
            foreach (var child in Children)
            {
                result = child.Execute(parameter);
            }
            return result;
        }
    }
}