using System;
using System.Text.RegularExpressions;

namespace CodeSharper.Interpreter.Common
{
    public class RegularExpressionClassElementSelector : ClassElementSelector, IEquatable<RegularExpressionClassElementSelector>
    {
        private readonly Regex regex;

        /// <summary>
        /// Gets the regex.
        /// </summary>
        public Regex Regex
        {
            get { return regex; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassElementSelector"/> class.
        /// </summary>
        public RegularExpressionClassElementSelector(String name) : base(name)
        {
            regex = new Regex(name);
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(RegularExpressionClassElementSelector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(regex, other.regex);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RegularExpressionClassElementSelector) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (regex != null ? regex.GetHashCode() : 0);
            }
        }

        #endregion

    }
}