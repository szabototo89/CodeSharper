using System;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public class ErrorMessage : IEquatable<ErrorMessage>
    {
        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ErrorMessage(Exception exception)
        {
            Exception = exception;
        }
        
        #region Equality members

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            return (Exception != null ? Exception.GetHashCode() : 0);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as ErrorMessage);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ErrorMessage other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this, true) ??
                   Exception == other.Exception;
        }

        #endregion
    }
}