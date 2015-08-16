using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes.Combinators;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Languages.CSharp.Combinators
{
    public class RelativeSyntaxNodeCombinator : BinaryCombinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeSyntaxNodeCombinator"/> class.
        /// </summary>
        public RelativeSyntaxNodeCombinator(CombinatorBase left, CombinatorBase right)
            : base(left, right)
        {
        }

        /// <summary>
        /// Calculates the specified values.
        /// </summary>
        public override IEnumerable<Object> Calculate(IEnumerable<Object> values)
        {
            var leftExpression = Left.Calculate(values).OfType<SyntaxNode>();

            if (leftExpression.Any())
            {
                foreach (var node in leftExpression)
                {
                    var children = node.DescendantNodes();
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
