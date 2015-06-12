using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class RelativeNodeCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeNodeCombinator"/> class.
        /// </summary>
        public RelativeNodeCombinator(CombinatorBase left, CombinatorBase right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var leftExpression = Left.Calculate(values).OfType<IHasChildren<Object>>();

            if (leftExpression.Any())
            {
                foreach (var node in leftExpression)
                {
                    var children = node.ToEnumerable();
                    foreach (var result in Right.Calculate(children))
                    {
                        yield return result;
                    }
                }
            }
            else
            {
                var elements = Right.Calculate(Left.Calculate(values));

                foreach (var element in elements)
                {
                    yield return element;
                }
            }
        }
    }
}