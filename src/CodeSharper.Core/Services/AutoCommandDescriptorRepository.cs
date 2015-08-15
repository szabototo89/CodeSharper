using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Utilities;
using static CodeSharper.Core.Utilities.ConstructsHelper;

namespace CodeSharper.Core.Services
{
    public class AutoCommandDescriptorRepository : IDescriptorRepository
    {
        private readonly IEnumerable<CommandDescriptor> commandDescriptors;

        public AutoCommandDescriptorRepository(IEnumerable<Type> runnableTypes)
        {
            Assume.IsRequired(runnableTypes, nameof(runnableTypes));
            commandDescriptors = RetrieveCommandDescriptors(runnableTypes);
        }

        IEnumerable<CombinatorDescriptor> IDescriptorRepository.GetCombinatorDescriptors()
        {
            yield break;
        }

        IEnumerable<ModifierDescriptor> IDescriptorRepository.GetModifierDescriptors()
        {
            yield break;
        }

        IEnumerable<SelectorDescriptor> IDescriptorRepository.GetSelectorDescriptors()
        {
            yield break;
        }

        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return commandDescriptors;
        }

        private IEnumerable<CommandDescriptor> RetrieveCommandDescriptors(IEnumerable<Type> runnableTypes)
        {
            var runnables = runnableTypes.Where(runnableType => typeof (IRunnable).IsAssignableFrom(runnableType));

            foreach (var type in runnables)
            {
                var attributes = type.GetCustomAttributes<CommandDescriptorAttribute>(true).ToArray();

                if (!attributes.Any()) continue;
                if (attributes.Length > 1)
                    throw new Exception($"Only one {nameof(CommandDescriptorAttribute)} is allowed to be defined in {type.FullName} runnable.");

                var attribute = attributes.Single();
                var commandArguments = RetrieveCommandArguments(type);
                var descriptor = new CommandDescriptor(type.Name, attribute.Description ?? String.Empty, commandArguments, Array(attribute.CommandName));
                yield return descriptor;
            }
        }

        private IEnumerable<ArgumentDescriptor> RetrieveCommandArguments(Type runnableType)
        {
            var runnableProperties = runnableType.GetProperties();
            var runnablePropertiesWithAttribute = runnableProperties.Select(property => new
            {
                PropertyInformation = property,
                Attribute = property.GetCustomAttributes(typeof (ParameterAttribute), true)
                                    .OfType<ParameterAttribute>()
                                    .SingleOrDefault()
            });
            var properties = runnablePropertiesWithAttribute.Where(property => property.Attribute != null);

            var position = 0;
            foreach (var property in properties)
            {
                var descriptor = new ArgumentDescriptor
                {
                    ArgumentName = property.Attribute.PropertyName,
                    ArgumentType = property.PropertyInformation.PropertyType,
                    DefaultValue = null,
                    IsOptional = true,
                    Position = position
                };

                yield return descriptor;

                position++;
            }
        }
    }
}