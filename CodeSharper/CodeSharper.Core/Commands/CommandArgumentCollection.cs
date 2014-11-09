using System;
using System.Collections;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public class CommandArgumentCollection : IEnumerable<CommandArgument>
    {
        private readonly Dictionary<String, CommandArgument> _arguments;

        public CommandArgumentCollection()
        {
            _arguments = new Dictionary<String, CommandArgument>();
        }

        public TValue GetArgumentValue<TValue>(String argumentName)
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

        public Option<CommandArgument> TryGetArgument(String argumentName)
        {
            CommandArgument argument;
            if (!_arguments.TryGetValue(argumentName, out argument))
                return Option.None;

            return Option.Some(argument);
        }
    }
}