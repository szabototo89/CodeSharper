using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class BinarySelector : BaseSelector, IEquatable<BinarySelector>
    {
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public BaseSelector Left { get; protected set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public BaseSelector Right { get; protected set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public BaseSelectorOperator Operator { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySelector"/> class.
        /// </summary>
        public BinarySelector(BaseSelector left, BaseSelector right, BaseSelectorOperator @operator)
        {
            Assume.NotNull(left, "left");
            Assume.NotNull(right, "right");
            Assume.NotNull(@operator, "operator");

            Left = left;
            Right = right;
            Operator = @operator;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(BinarySelector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Left, other.Left) && Equals(Right, other.Right) && Equals(Operator, other.Operator);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as BinarySelector);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = (Left != null ? Left.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Right != null ? Right.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Operator != null ? Operator.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

    }
}