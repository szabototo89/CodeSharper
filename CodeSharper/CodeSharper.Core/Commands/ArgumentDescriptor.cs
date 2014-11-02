using System;

namespace CodeSharper.Core.Commands
{
    public struct ArgumentDescriptor
    {
        public String ArgumentName { get; set; }

        public Boolean IsOptional { get; set; }

        public Object DefaultValue { get; set; }

        public Type ArgumentType { get; set; }
    }
}