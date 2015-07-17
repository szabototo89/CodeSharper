using System;

namespace CodeSharper.Interpreter.Common
{
    public class ClassElementSelector : IEquatable<ClassElementSelector>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassElementSelector"/> class.
        /// </summary>
        public ClassElementSelector(String name)
        {
            Name = name;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ClassElementSelector other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(Name, other.Name);
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
            return Equals((ClassElementSelector) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #endregion
    }
}