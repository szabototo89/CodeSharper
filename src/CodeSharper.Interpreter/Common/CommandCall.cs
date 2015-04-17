using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Interpreter.Common
{
    public class CommandCall : IEquatable<CommandCall>
    {
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public String MethodName { get; protected set; }

        /// <summary>
        /// Gets or sets the actual parameters.
        /// </summary>
        public IEnumerable<ActualParameter> ActualParameters { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCall"/> class.
        /// </summary>
        public CommandCall(String methodName, IEnumerable<ActualParameter> actualParameters)
        {
            Assume.NotNull(methodName, "methodName");
            Assume.NotNull(actualParameters, "actualParameters");

            MethodName = methodName;
            ActualParameters = actualParameters;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(CommandCall other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(MethodName, other.MethodName) && 
                   Equals(ActualParameters, other.ActualParameters);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as CommandCall);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((MethodName != null ? MethodName.GetHashCode() : 0) * 397) ^ (ActualParameters != null ? ActualParameters.GetHashCode() : 0);
            }
        }

        #endregion
    }
}