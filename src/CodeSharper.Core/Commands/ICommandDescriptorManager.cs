using System.Collections.Generic;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Commands
{
    public interface ICommandDescriptorManager
    {
        /// <summary>
        /// Registers the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        void Register(CommandDescriptor descriptor);

        /// <summary>
        /// Gets the command descriptors.
        /// </summary>
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}