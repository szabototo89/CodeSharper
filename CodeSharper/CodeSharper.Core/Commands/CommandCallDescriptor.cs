using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public class CommandCallDescriptor
    {
        public CommandCallDescriptor(String name, IEnumerable<Object> arguments = null, Dictionary<String, Object> namedArguments = null)
        {
            Name = name ?? String.Empty;
            Arguments = arguments ?? Enumerable.Empty<Object>();
            NamedArguments = namedArguments ?? new Dictionary<String, Object>();
        }

        public String Name { get; private set; }

        public IEnumerable<Object> Arguments { get; private set; }

        public Dictionary<String, Object> NamedArguments { get; private set; }
    }
}
