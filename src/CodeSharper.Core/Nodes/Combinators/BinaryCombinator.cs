using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Nodes.Combinators
{
    public abstract class BinaryCombinator : CombinatorBase
    {
        /// <summary>
        /// Gets or sets the left expression of operand
        /// </summary>
        public CombinatorBase Left { get; }

        /// <summary>
        /// Gets or sets the right expression of operand
        /// </summary>
        public CombinatorBase Right { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryCombinator"/> class.
        /// </summary>
        protected BinaryCombinator(CombinatorBase left, CombinatorBase right)
        {
            Assume.NotNull(left, nameof(left));
            Assume.NotNull(right, nameof(right));

            Left = left;
            Right = right;
        }
    }
}