using System;

namespace CodeSharper.Core.Common.Runnables.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConsumesAttribute : Attribute
    {
        public Type TypeOfArgumentUnwrapper { get; protected set; }

        public ConsumesAttribute(Type typeOfArgumentUnwrapper)
        {
            TypeOfArgumentUnwrapper = typeOfArgumentUnwrapper;
        }
    }
}