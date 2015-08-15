using System;
using NUnit.Framework;

namespace CodeSharper.Tests.TestAttributes
{
    public class MethodNameAttribute : Attribute
    {
        public String MethodName { get; }

        public MethodNameAttribute(String methodName)
        {
            MethodName = methodName;
        }

        #region Equality members

        protected Boolean Equals(MethodNameAttribute other)
        {
            return base.Equals(other) && String.Equals(MethodName, other.MethodName);
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MethodNameAttribute) obj);
        }

        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (MethodName != null ? MethodName.GetHashCode() : 0);
            }
        }

        #endregion
    }

    public class MethodTestAttribute : TestAttribute
    {
        private readonly String METHOD_SUFFIX = "Method";
        public String MethodName { get; set; }

        public MethodTestAttribute(String methodName)
        {
            MethodName = TransformMethodName(methodName);
            Description = $"{MethodName} {Description}";
        }

        private String TransformMethodName(String methodName)
        {
            if (methodName?.EndsWith(METHOD_SUFFIX) == true)
            {
                var index = methodName.LastIndexOf(METHOD_SUFFIX);
                return methodName.Remove(index);
            }

            return methodName;
        }
    }
}