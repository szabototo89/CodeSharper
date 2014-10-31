using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common
{
    public sealed class Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
    {
        private readonly TLeft _left;
        private readonly TRight _right;

        public Either(TLeft left)
        {
            HasLeft = true;
            HasRight = false;
            _left = left;
        }

        public Either(TRight right)
        {
            HasLeft = false;
            HasRight = true;
            _right = right;
        }

        public Boolean HasLeft { get; private set; }

        public Boolean HasRight { get; private set; }

        public TLeft Left { get { return _left; } }

        public TRight Right { get { return _right; } }

        public Boolean Equals(Either<TLeft, TRight> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return HasLeft.Equals(other.HasLeft) && HasRight.Equals(other.HasRight) && EqualityComparer<TLeft>.Default.Equals(_left, other._left) && EqualityComparer<TRight>.Default.Equals(_right, other._right);
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Either<TLeft, TRight> && Equals((Either<TLeft, TRight>)obj);
        }

        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = HasLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ HasRight.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<TLeft>.Default.GetHashCode(_left);
                hashCode = (hashCode * 397) ^ EqualityComparer<TRight>.Default.GetHashCode(_right);
                return hashCode;
            }
        }

        public static Boolean operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
        {
            return Equals(left, right);
        }

        public static Boolean operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right)
        {
            return !Equals(left, right);
        }

        public static implicit operator Either<TLeft, TRight>(TLeft left)
        {
            return new Either<TLeft, TRight>(left);
        }

        public static implicit operator Either<TLeft, TRight>(TRight right)
        {
            return new Either<TLeft, TRight>(right);
        }

        public override string ToString()
        {
            return string.Format("Left: {0}, Right: {1}, HasLeft: {2}, HasRight: {3}", Left, Right, HasLeft, HasRight);
        }
    }
}
