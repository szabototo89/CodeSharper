using System;
using System.Collections;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands
{
    public class CommandArgumentCollection : IEnumerable<CommandArgument>
    {
        private readonly Dictionary<String, CommandArgument> _namedArguments;

        public CommandArgumentCollection()
        {
            _namedArguments = new Dictionary<String, CommandArgument>();
        }

        public TValue GetArgumentValue<TValue>(String argumentName)
        {
            return ((TValue)_namedArguments[argumentName].Value);
        }

        public CommandArgumentCollection SetArgument(String argumentName, Object value)
        {
            var argument = new CommandArgument { Name = argumentName, Value = value };
            if (_namedArguments.ContainsKey(argumentName))
                _namedArguments[argumentName] = argument;
            else _namedArguments.Add(argumentName, argument);

            return this;
        }

        public IEnumerator<CommandArgument> GetEnumerator()
        {
            return _namedArguments.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Option<CommandArgument> TryGetArgument(String argumentName)
        {
            CommandArgument argument;
            if (!_namedArguments.TryGetValue(argumentName, out argument))
                return Option.None;

            return Option.Some(argument);
        }
    }
}