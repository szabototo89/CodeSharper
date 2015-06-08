using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Commands.Selectors;
using CodeSharper.Core.Common;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Utilities;
using SelectorDescriptor = CodeSharper.Core.Nodes.Selectors.SelectorDescriptor;

namespace CodeSharper.Core.Services
{
    public class MemoryDescriptorRepository : IDescriptorRepository
    {
        private readonly IEnumerable<CommandDescriptor> commandDescriptors;
        private readonly IEnumerable<CombinatorDescriptor> combinatorDescriptors;
        private readonly IEnumerable<SelectorDescriptor> selectorDescriptors;
        private readonly IEnumerable<ModifierDescriptor> modifierDescriptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryDescriptorRepository"/> class.
        /// </summary>
        public MemoryDescriptorRepository(IEnumerable<SelectorDescriptor> selectorDescriptors = null, IEnumerable<CombinatorDescriptor> combinatorDescriptors = null, IEnumerable<ModifierDescriptor> modifierDescriptors = null, IEnumerable<CommandDescriptor> commandDescriptors = null)
        {
            this.combinatorDescriptors = combinatorDescriptors.GetOrEmpty();
            this.selectorDescriptors = selectorDescriptors.GetOrEmpty();
            this.modifierDescriptors = modifierDescriptors.GetOrEmpty();
            this.commandDescriptors = commandDescriptors.GetOrEmpty();
        }

        /// <summary>
        /// Gets the combinator descriptors.
        /// </summary>
        public IEnumerable<CombinatorDescriptor> GetCombinatorDescriptors()
        {
            return combinatorDescriptors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the pseudo selectors.
        /// </summary>
        public IEnumerable<ModifierDescriptor> GetModifierDescriptors()
        {
            return modifierDescriptors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the selectors.
        /// </summary>
        public IEnumerable<SelectorDescriptor> GetSelectorDescriptors()
        {
            return selectorDescriptors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the command descriptors.
        /// </summary>
        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return commandDescriptors.ToList().AsReadOnly();
        }
    }
}