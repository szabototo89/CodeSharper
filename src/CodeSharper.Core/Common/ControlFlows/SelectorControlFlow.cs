using System;
using System.Collections;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Core.Common.ControlFlows
{
    public class SelectorControlFlow : ControlFlowBase
    {
        public CombinatorBase Combinator { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorControlFlow"/> class.
        /// </summary>
        public SelectorControlFlow(CombinatorBase combinator)
        {
            Assume.NotNull(combinator, "combinator");

            Combinator = new RelativeNodeCombinator(new UniversalCombinator(), combinator);
        }

        /// <summary>
        /// Executes the specified parameter
        /// </summary>
        public override Object Execute(Object parameter)
        {
            if (!(parameter is String) && parameter is IEnumerable)
            {
                return Combinator.Calculate(((IEnumerable)parameter).OfType<Object>());
            }

            return Combinator.Calculate(new[] { parameter });
        }
    }
}