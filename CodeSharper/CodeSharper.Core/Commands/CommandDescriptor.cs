using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Commands
{
    public struct CommandDescriptor 
    {
        public String Name { get; set; }

        public IEnumerable<ArgumentDescriptor> Arguments { get; set; }
    }
}