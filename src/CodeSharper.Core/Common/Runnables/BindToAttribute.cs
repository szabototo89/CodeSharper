using System;

namespace CodeSharper.Core.Common.Runnables
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BindToAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public String PropertyName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindToAttribute"/> class.
        /// </summary>
        public BindToAttribute(String propertyName)
        {
            PropertyName = propertyName;
        }
    }
}