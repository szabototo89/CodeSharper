using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    public struct RunnableDescriptor
    {
        private readonly List<IArgumentWrapper> _supportedArgumentWrappers;

        private readonly List<IArgumentUnwrapper> _supportedArgumentUnwrappers;
        private readonly IEnumerable<Attribute> _attributes;

        public Type Type { get; private set; }

        public RunnableDescriptor(Type type)
            : this()
        {
            Constraints.NotNull(() => type);
            Type = type;

            _supportedArgumentWrappers = new List<IArgumentWrapper>();
            _supportedArgumentUnwrappers = new List<IArgumentUnwrapper>();
            _attributes = Type.GetCustomAttributes(true).OfType<Attribute>();

            HandleArgumentWrappers();
            HandleArgumentUnwrappers();
        }

        private void HandleArgumentUnwrappers()
        {
            _supportedArgumentUnwrappers.AddRange(
                _attributes.OfType<ConsumesAttribute>()
                    .Select(attribute => attribute.TypeOfArgumentUnwrapper)
                    .Select(type => (IArgumentUnwrapper)Activator.CreateInstance(type))
                );
        }

        private void HandleArgumentWrappers()
        {
            _supportedArgumentWrappers.AddRange(
                _attributes.OfType<ProducesAttribute>()
                    .Select(attribute => attribute.TypeOfArgumentWrapper)
                    .Select(type => (IArgumentWrapper)Activator.CreateInstance(type))
                );
        }

        public IEnumerable<IArgumentWrapper> SupportedArgumentWrappers { get { return _supportedArgumentWrappers.AsReadOnly(); } }

        public IEnumerable<IArgumentUnwrapper> SupportedArgumentUnwrappers { get { return _supportedArgumentUnwrappers.AsReadOnly(); } }
    }
}