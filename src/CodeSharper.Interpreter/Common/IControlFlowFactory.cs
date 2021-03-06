﻿using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Interpreter.Common
{
    public interface IControlFlowFactory<out TControlFlow>
    {
        /// <summary>
        /// Creates the specified control flow.
        /// </summary>
        TControlFlow Create(ControlFlowElementBase controlFlow);

        /// <summary>
        /// Creates the specified sequence.
        /// </summary>
        TControlFlow Create(SequenceControlFlowElement sequence);

        /// <summary>
        /// Creates the specified pipeline.
        /// </summary>
        TControlFlow Create(PipelineControlFlowElement pipeline);

        /// <summary>
        /// Creates the specified command call.
        /// </summary>
        TControlFlow Create(CommandCallControlFlowElement commandCall);
    }
}


