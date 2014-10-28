using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    public struct RunnableDescriptor
    {
        private readonly List<IArgumentAfter> _supportedArgumentWrappers;

        private readonly List<IArgumentBefore> _supportedArgumentUnwrappers;
        private readonly IEnumerable<Attribute> _attributes;

        public Type Type { get; private set; }

        public RunnableDescriptor(Type type)
            : this()
        {
            Constraints.NotNull(() => type);
            Type = type;

            _supportedArgumentWrappers = new List<IArgumentAfter>();
            _supportedArgumentUnwrappers = new List<IArgumentBefore>();
            _attributes = Type.GetCustomAttributes(true).OfType<Attribute>();

            HandleArgumentWrappers();
            HandleArgumentUnwrappers();
        }

        private void HandleArgumentUnwrappers()
        {
            _supportedArgumentUnwrappers.AddRange(
                _attributes.OfType<ConsumesAttribute>()
                    .Select(attribute => attribute.TypeOfArgumentUnwrapper)
                    .Select(type => (IArgumentBefore)Activator.CreateInstance(type))
                );
        }

        private void HandleArgumentWrappers()
        {
            _supportedArgumentWrappers.AddRange(
                _attributes.OfType<ProducesAttribute>()
                    .Select(attribute => attribute.TypeOfArgumentWrapper)
                    .Select(type => (IArgumentAfter)Activator.CreateInstance(type))
                );
        }

        public IEnumerable<IArgumentAfter> SupportedArgumentAfters { get { return _supportedArgumentWrappers.AsReadOnly(); } }

        public IEnumerable<IArgumentBefore> SupportedArgumentBefores { get { return _supportedArgumentUnwrappers.AsReadOnly(); } }
    }
}