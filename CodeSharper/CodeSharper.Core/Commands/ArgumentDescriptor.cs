using System;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public class ArgumentDescriptor
    {
        public Boolean IsOptional { get; set; }

        public Object DefaultValue { get; set; }

        public Type ArgumentType { get; set; }

        public String ArgumentName { get; set; }
    }
}