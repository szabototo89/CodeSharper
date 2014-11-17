using System;

namespace CodeSharper.Core.Commands.CommandFactories
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BindToAttribute : Attribute
    {
        public String PropertyName { get; private set; }

        public BindToAttribute(String propertyName)
        {
            PropertyName = propertyName;
        }
    }
}