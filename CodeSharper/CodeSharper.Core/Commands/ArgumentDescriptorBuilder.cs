using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Commands
{
    public class ArgumentDescriptorBuilder
    {
        private readonly List<ArgumentDescriptor> _descriptors;

        public ArgumentDescriptorBuilder()
        {
            _descriptors  = new List<ArgumentDescriptor>();
        }

        public IEnumerable<ArgumentDescriptor> Create()
        {
            var result = _descriptors.ToArray();
            _descriptors.Clear();
            return result;
        }

        public ArgumentDescriptorBuilder Argument<TArgument>(String argumentName, Boolean isOptional = false, TArgument defaultValue = default(TArgument))
        {
            _descriptors.Add(new ArgumentDescriptor()
            {
                ArgumentName = argumentName,
                ArgumentType = typeof(TArgument),
                IsOptional = isOptional,
                DefaultValue = defaultValue
            });

            return this;
        }
    }
}
