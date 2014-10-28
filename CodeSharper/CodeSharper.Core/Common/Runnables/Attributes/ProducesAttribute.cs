using System;

namespace CodeSharper.Core.Common.Runnables.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ProducesAttribute : Attribute
    {
        public Type TypeOfArgumentWrapper { get; protected set; }

        public ProducesAttribute(Type typeOfArgumentWrapper)
        {
            TypeOfArgumentWrapper = typeOfArgumentWrapper;
        }
    }
}