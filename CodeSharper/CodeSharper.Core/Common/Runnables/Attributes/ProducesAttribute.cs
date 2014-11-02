using System;

namespace CodeSharper.Core.Common.Runnables.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ProducesAttribute : Attribute
    {
        public Type TypeOfArgumentAfter { get; protected set; }

        public ProducesAttribute(Type typeOfArgumentAfter)
        {
            TypeOfArgumentAfter = typeOfArgumentAfter;
        }
    }
}