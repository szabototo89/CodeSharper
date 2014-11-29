using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ConstraintChecking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeSharper.Core.Utilities
{
    public class JsonCommandDescriptorParser : ICommandDescriptorParser
    {
        private readonly string COMMAND_ARGUMENT_DEFAULT_VALUE = "default-value";
        private readonly string COMMAND_NAME = "name";
        private readonly string COMMAND_NAMES = "command-names";
        private readonly string COMMAND_DESCRIPTION = "description";
        private readonly string COMMAND_ARGUMENTS = "arguments";
        private readonly string COMMAND_ARGUMENT_NAME = "name";
        private readonly string COMMAND_ARGUMENT_OPTIONAL = "optional";
        private readonly string COMMAND_ARGUMENT_TYPE = "type";

        // {
        //     "name": "Find Text Command",
        //     "command-names": [ "find-text" ],
        //     "arguments": [
        //         {
        //             "name": "pattern",
        //             "type": "System.String",
        //             "description": "Some description goes here",
        //             "optional": false,
        //             "default-value": null
        //         }
        //     ]
        // }

        public CommandDescriptor Parse(String source)
        {
            Constraints.NotNull(() => source);

            var body = JObject.Parse(source);

            return new CommandDescriptor() {
                Name = body[COMMAND_NAME].Value<String>(),
                CommandNames = body[COMMAND_NAMES].Values<String>(),
                Description = _GetValueOrDefault<String>(body, COMMAND_DESCRIPTION),
                Arguments = Enumerable.Cast<JObject>(body[COMMAND_ARGUMENTS].ToArray())
                    .Select(arg => new ArgumentDescriptor {
                        ArgumentName = _GetValueOrDefault<String>(arg, COMMAND_ARGUMENT_NAME),
                        DefaultValue = _ParseDefaultValue(_GetValueOrDefault<JToken>(arg, COMMAND_ARGUMENT_DEFAULT_VALUE)),
                        IsOptional = _GetValueOrDefault<Boolean>(arg, COMMAND_ARGUMENT_OPTIONAL),
                        ArgumentType = _ResolveType(_GetValueOrDefault<String>(arg, COMMAND_ARGUMENT_TYPE))
                    }).ToArray()
            };
        }

        private TResult _GetValueOrDefault<TResult>(JObject argument, String propertyName, TResult defaultValue = default(TResult))
        {
            JToken result;
            if (!argument.TryGetValue(propertyName, out result))
                return defaultValue;

            return result.Value<TResult>();
        }

        private Object _ParseDefaultValue(JToken value)
        {
            if (!value.HasValues)
                return null;

            return value.Value<Object>();
        }

        private Type _ResolveType(String typeName)
        {
            return Type.GetType(typeName, throwOnError: false, ignoreCase: true);
        }

        public static CommandDescriptor ParseFrom(String source)
        {
            return new JsonCommandDescriptorParser().Parse(source);
        }
    }
}
