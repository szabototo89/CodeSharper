using System;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public abstract class ArgumentDescriptorBase
    {
        public Boolean IsOptional { get; set; }

        public Object DefaultValue { get; set; }

        public Type ArgumentType { get; set; }
    }

    public class PositionedArgumentDescriptor : ArgumentDescriptorBase
    {
        public Int32 ArgumentPosition { get; set; }
    }

    public class NamedArgumentDescriptor : ArgumentDescriptorBase
    {
        public String ArgumentName { get; set; }
    }
}