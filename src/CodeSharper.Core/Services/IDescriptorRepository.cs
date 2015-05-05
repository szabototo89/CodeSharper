using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Core.Services
{
    public interface IDescriptorRepository
    {
        /// <summary>
        /// Loads the combinators.
        /// </summary>
        IEnumerable<CombinatorDescriptor> LoadCombinators();

        /// <summary>
        /// Loads the selectors.
        /// </summary>
        IEnumerable<SelectorDescriptor> LoadSelectors();
    }
}

