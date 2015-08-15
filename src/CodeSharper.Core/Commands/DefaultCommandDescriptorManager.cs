using System.Collections.Generic;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Commands
{
    public class DefaultCommandDescriptorManager : ICommandDescriptorManager
    {
        private readonly List<CommandDescriptor> commandDescriptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCommandDescriptorManager"/> class.
        /// </summary>
        public DefaultCommandDescriptorManager()
        {
            commandDescriptors = new List<CommandDescriptor>();
        }

        /// <summary>
        /// Registers the specified descriptor.
        /// </summary>
        public void Register(CommandDescriptor descriptor)
        {
            commandDescriptors.Add(descriptor);
        }

        /// <summary>
        /// Gets the command descriptors.
        /// </summary>
        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return commandDescriptors.AsReadOnly();
        }
    }
}