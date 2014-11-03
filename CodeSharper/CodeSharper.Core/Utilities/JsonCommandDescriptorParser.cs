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
        // {
        //     "name": "Find Text Command",
        //     "command-names": [ "find-text" ],
        //     "arguments": [
        //         {
        //             "name": "pattern",
        //             "type": "System.String",
        //             "optional": false,
        //             "default-typeName": null
        //         }
        //     ]
        // }

        public CommandDescriptor Parse(String source)
        {
            Constraints.NotNull(() => source);

            var body = JObject.Parse(source);
            return new CommandDescriptor()
            {
                Name = body["name"].Value<String>(),
                CommandNames = body["command-names"].Values<String>(),
                Arguments = body["arguments"]
                    .Values()
                    .Select(arg => new ArgumentDescriptor()
                    {
                        ArgumentName = arg["name"].Value<String>(),
                        DefaultValue = arg["default-typeName"].Value<Object>(),
                        IsOptional = arg["optional"].Value<Boolean>(),
                        ArgumentType = ResolveType(arg["type"].Value<String>())
                    })
            };
        }

        private Type ResolveType(string typeName)
        {
            return Type.GetType(typeName, throwOnError: false, ignoreCase: true);
        }
    }
}
