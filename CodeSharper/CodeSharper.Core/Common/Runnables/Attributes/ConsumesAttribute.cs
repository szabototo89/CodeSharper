using System;

namespace CodeSharper.Core.Common.Runnables.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConsumesAttribute : Attribute
    {
        public Type TypeOfArgumentBefore { get; protected set; }

        public ConsumesAttribute(Type typeOfArgumentBefore)
        {
            TypeOfArgumentBefore = typeOfArgumentBefore;
        }
    }
}