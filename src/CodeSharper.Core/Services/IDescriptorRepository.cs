using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands.Selectors;
using CodeSharper.Core.Common;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using SelectorDescriptor = CodeSharper.Core.Nodes.Selectors.SelectorDescriptor;

namespace CodeSharper.Core.Services
{
    public interface IDescriptorRepository
    {
        /// <summary>
        /// Gets the combinators.
        /// </summary>
        IEnumerable<CombinatorDescriptor> GetCombinators();

        /// <summary>
        /// Gets the pseudo selectors.
        /// </summary>
        IEnumerable<ModifierDescriptor> GetPseudoSelectors();

        /// <summary>
        /// Gets the selectors.
        /// </summary>
        IEnumerable<SelectorDescriptor> GetSelectors();

        /// <summary>
        /// Gets the command descriptors.
        /// </summary>
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}

