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
        private readonly List<IArgumentAfter> _supportedArgumentAfters;

        private readonly List<IArgumentBefore> _supportedArgumentBefores;
        private readonly IEnumerable<Attribute> _attributes;

        public Type Type { get; private set; }

        public RunnableDescriptor(Type type)
            : this()
        {
            Constraints.NotNull(() => type);
            Type = type;

            _supportedArgumentAfters = new List<IArgumentAfter>();
            _supportedArgumentBefores = new List<IArgumentBefore>();
            _attributes = Type.GetCustomAttributes(true).OfType<Attribute>();

            HandleArgumentAfters();
            HandleArgumentBefores();
        }

        private void HandleArgumentBefores()
        {
            _supportedArgumentBefores.AddRange(
                _attributes.OfType<ConsumesAttribute>()
                    .Select(attribute => attribute.TypeOfArgumentBefore)
                    .Select(type => (IArgumentBefore)Activator.CreateInstance(type))
                );
        }

        private void HandleArgumentAfters()
        {
            _supportedArgumentAfters.AddRange(
                _attributes.OfType<ProducesAttribute>()
                    .Select(attribute => attribute.TypeOfArgumentAfter)
                    .Select(type => (IArgumentAfter)Activator.CreateInstance(type))
                );
        }

        public IEnumerable<IArgumentAfter> SupportedArgumentAfters { get { return _supportedArgumentAfters.AsReadOnly(); } }

        public IEnumerable<IArgumentBefore> SupportedArgumentBefores { get { return _supportedArgumentBefores.AsReadOnly(); } }
    }
}