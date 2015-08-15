using System;

namespace CodeSharper.Core.Common.Runnables
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public String PropertyName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/> class.
        /// </summary>
        public ParameterAttribute(String propertyName)
        {
            PropertyName = propertyName;
        }
    }
}