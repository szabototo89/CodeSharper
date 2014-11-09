using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Commands
{
    public class CommandDescriptor
    {
        public static readonly CommandDescriptor Empty;

        static CommandDescriptor()
        {
            Empty = new CommandDescriptor();
        }

        public String Name { get; set; }

        public IEnumerable<ArgumentDescriptorBase> Arguments { get; set; }
        
        public IEnumerable<String> CommandNames { get; set; }

        public String Description { get; set; }

        public CommandDescriptor()
        {
            Arguments = Enumerable.Empty<ArgumentDescriptorBase>();
            CommandNames = Enumerable.Empty<String>();
        }
    }
}