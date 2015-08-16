using System;
using System.Reflection;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.CSharp.Combinators;

namespace CodeSharper.Tests.Languages.CSharp.Runnables
{
    public class CombinatorBuilder
    {
        private CombinatorBase combinator;

        public CombinatorBuilder Select(SelectorBase selector)
        {
            combinator = new SelectionCombinator(selector);
            return this;
        }

        public CombinatorBuilder And(SelectorBase selector)
        {
            combinator = new RelativeSyntaxNodeCombinator(combinator, new SelectionCombinator(selector));
            return this;
        }

        public CombinatorBuilder And<TBinaryCombinator>(SelectorBase selector)
            where TBinaryCombinator : BinaryCombinator
        {
            combinator = (TBinaryCombinator) Activator.CreateInstance(typeof (TBinaryCombinator), combinator, new SelectionCombinator(selector));
            return this;
        }

        public CombinatorBase Build()
        {
            return combinator;
        }
    }
}