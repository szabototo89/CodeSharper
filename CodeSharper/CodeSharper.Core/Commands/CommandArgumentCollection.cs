using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeSharper.Core.Commands
{
    public class CommandArgumentCollection : IEnumerable<CommandArgument>
    {
        private readonly Dictionary<String, CommandArgument> _arguments;

        public CommandArgumentCollection()
        {
            _arguments = new Dictionary<String, CommandArgument>();
        }

        public TValue GetArgument<TValue>(String argumentName)
        {
            return ((TValue)_arguments[argumentName].Value);
        }

        public CommandArgumentCollection SetArgument(String argumentName, Object value)
        {
            var argument = new CommandArgument { Name = argumentName, Value = value };
            if (_arguments.ContainsKey(argumentName))
                _arguments[argumentName] = argument;
            else _arguments.Add(argumentName, argument);

            return this;
        }

        public IEnumerator<CommandArgument> GetEnumerator()
        {
            return _arguments.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}