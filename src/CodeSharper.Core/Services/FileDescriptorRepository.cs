using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using CodeSharper.Core.Commands.Selectors;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Utilities;
using SelectorDescriptor = CodeSharper.Core.Nodes.Selectors.SelectorDescriptor;

namespace CodeSharper.Core.Services
{
    public class FileDescriptorRepository : IDescriptorRepository
    {
        private const String ELEMENT_TYPE_SELECTOR = "element-type-selector";
        private const String COMBINATOR_SELECTOR = "combinator";
        private const String MODIFIER_SELECTOR = "pseudo-selector";

        private readonly IEnumerable<Assembly> predefinedAssemblies;
        private readonly IEnumerable<Assembly> assemblies;
        private readonly DataContractJsonSerializer serializer;

        private readonly List<CombinatorDescriptor> combinators;
        private readonly List<SelectorDescriptor> selectors;
        private readonly List<ModifierDescriptor> pseudoSelectors;
        private readonly List<CommandDescriptor> commandDescriptors;

        private enum DescriptorType
        {
            Selector,
            Combinator,
            ModifierSelector
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDescriptorRepository"/> class.
        /// </summary>
        public FileDescriptorRepository(String fileName, IEnumerable<Assembly> assemblies = null)
        {
            Assume.NotNull(fileName, nameof(fileName));
            Assume.FileExists(fileName, nameof(fileName));

            serializer = new DataContractJsonSerializer(typeof (DescriptorModel));
            combinators = new List<CombinatorDescriptor>();
            selectors = new List<SelectorDescriptor>();
            pseudoSelectors = new List<ModifierDescriptor>();
            commandDescriptors = new List<CommandDescriptor>();
            predefinedAssemblies = new[] {Assembly.Load("mscorlib")};

            this.assemblies = assemblies ?? new[] {Assembly.GetExecutingAssembly()};

            loadFromFile(fileName);
        }

        private void loadFromFile(String fileName)
        {
            try
            {
                var reader = File.OpenRead(fileName);
                var descriptor = (DescriptorModel) serializer.ReadObject(reader);

                selectors.Clear();
                combinators.Clear();
                pseudoSelectors.Clear();
                commandDescriptors.Clear();

                foreach (var commandDescriptor in descriptor.CommandDescriptors)
                {
                    var desc = new CommandDescriptor
                    {
                        Name = commandDescriptor.Name,
                        Description = commandDescriptor.Description,
                        CommandNames = new List<String>(commandDescriptor.CommandNames)
                    };

                    desc.Arguments = commandDescriptor.Arguments.Select((ArgumentDescriptorModel arg) => new ArgumentDescriptor
                    {
                        ArgumentName = arg.Name,
                        DefaultValue = arg.DefaultValue,
                        IsOptional = arg.IsOptional,
                        Position = arg.Position,
                        ArgumentType = findInAssemblies(arg.ArgumentType)
                    });

                    commandDescriptors.Add(desc);
                }

                foreach (var selectionDescriptors in descriptor.SelectionDescriptors)
                {
                    switch (selectionDescriptors.SelectorType)
                    {
                        case ELEMENT_TYPE_SELECTOR:
                        {
                            var type = findInAssemblies(selectionDescriptors.Type, DescriptorType.Selector);
                            selectors.Add(new SelectorDescriptor(selectionDescriptors.Name, selectionDescriptors.Value, type));
                            break;
                        }
                        case COMBINATOR_SELECTOR:
                        {
                            var type = findInAssemblies(selectionDescriptors.Type, DescriptorType.Combinator);
                            combinators.Add(new CombinatorDescriptor(selectionDescriptors.Name, selectionDescriptors.Value, type));
                            break;
                        }
                        case MODIFIER_SELECTOR:
                        {
                            var type = findInAssemblies(selectionDescriptors.Type, DescriptorType.ModifierSelector);

                            pseudoSelectors.Add(
                                new ModifierDescriptor(selectionDescriptors.Name,
                                                       selectionDescriptors.Value,
                                                       selectionDescriptors.Arguments, 
                                                       type,
                                                       selectionDescriptors.IsClassSelector));
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("Cannot parse descriptor file: {0}", fileName), exception);
            }
        }

        private Type findInAssemblies(String argumentType)
        {
            var definedAssemblies = assemblies.Union(predefinedAssemblies);

            var assemblyTypes = definedAssemblies.SelectMany(assembly => assembly.GetTypes());
            var matchedTypes = assemblyTypes.Where(type => type.FullName == argumentType || type.Name == argumentType).ToArray();

            if (!matchedTypes.Any())
                throw new Exception(String.Format("Not found argument type: {0}.", argumentType));
            if (matchedTypes.Length > 1)
                throw new Exception(String.Format("Ambiguation between found types: {0}", argumentType));

            return matchedTypes.Single();
        }

        private Type findInAssemblies(String typeName, DescriptorType descriptorType)
        {
            Type assignableFromType;
            switch (descriptorType)
            {
                case DescriptorType.Combinator:
                    assignableFromType = typeof (CombinatorBase);
                    break;
                case DescriptorType.Selector:
                    assignableFromType = typeof (SelectorBase);
                    break;
                case DescriptorType.ModifierSelector:
                    assignableFromType = typeof (ModifierBase);
                    break;
                default:
                    throw new NotSupportedException(String.Format("Not supported descriptor type: {0}", descriptorType));
            }

            var assemblyTypes = assemblies.SelectMany(assembly => assembly.GetTypes());
            var descriptorTypes = assemblyTypes.Where(type => type != assignableFromType && assignableFromType.IsAssignableFrom(type));
            var matchedTypes = descriptorTypes.Where(type => type.FullName == typeName || type.Name == typeName);

            if (!matchedTypes.Any())
                throw new Exception(String.Format("Not found descriptor type: {0}.", typeName));

            var result = matchedTypes.SingleOrDefault();

            if (result == null)
                throw new Exception("Ambiguation between descriptor types.");

            return result;
        }

        /// <summary>
        /// Gets the combinators.
        /// </summary>
        public IEnumerable<CombinatorDescriptor> GetCombinatorDescriptors()
        {
            return combinators.AsReadOnly();
        }

        /// <summary>
        /// Gets the pseudo selectors.
        /// </summary>
        public IEnumerable<ModifierDescriptor> GetModifierDescriptors()
        {
            return pseudoSelectors.AsReadOnly();
        }

        /// <summary>
        /// Gets the selectors.
        /// </summary>
        public IEnumerable<SelectorDescriptor> GetSelectorDescriptors()
        {
            return selectors.AsReadOnly();
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