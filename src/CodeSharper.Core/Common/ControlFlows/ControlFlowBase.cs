using System;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ControlFlowBase
    {
        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public abstract Object Execute(Object parameter);
    }

    public class SelectorControlFlowBase : ControlFlowBase
    {
        public CombinatorBase Combinator { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorControlFlowBase"/> class.
        /// </summary>
        public SelectorControlFlowBase(CombinatorBase combinator)
        {
            Assume.NotNull(combinator, "combinator");

            Combinator = combinator;
        }

        /// <summary>
        /// Executes the specified parameter
        /// </summary>
        public override Object Execute(Object parameter)
        {
            return Combinator.Calculate(new[] { parameter });
        }
    }
}