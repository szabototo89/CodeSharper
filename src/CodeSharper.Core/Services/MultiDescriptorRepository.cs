using System;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Core.Services
{
    public class MultiDescriptorRepository : IDescriptorRepository
    {
        private readonly IEnumerable<IDescriptorRepository> repositories;

        public MultiDescriptorRepository(IEnumerable<IDescriptorRepository> repositories)
        {
            Assume.NotNull(repositories, nameof(repositories));
            this.repositories = repositories;
        }

        private IEnumerable<TDescriptor> GetDescriptors<TDescriptor>(Func<IDescriptorRepository, IEnumerable<TDescriptor>> selectorFunction)
        {
            foreach (var repository in repositories)
            {
                foreach (var descriptor in selectorFunction(repository))
                {
                    yield return descriptor;
                }
            }
        }

        public IEnumerable<CombinatorDescriptor> GetCombinatorDescriptors()
        {
            return GetDescriptors(repository => repository.GetCombinatorDescriptors());
        }

        public IEnumerable<ModifierDescriptor> GetModifierDescriptors()
        {
            return GetDescriptors(repository => repository.GetModifierDescriptors());
        }

        public IEnumerable<SelectorDescriptor> GetSelectorDescriptors()
        {
            return GetDescriptors(repository => repository.GetSelectorDescriptors());
        }

        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return GetDescriptors(repository => repository.GetCommandDescriptors());
        }
    }
}