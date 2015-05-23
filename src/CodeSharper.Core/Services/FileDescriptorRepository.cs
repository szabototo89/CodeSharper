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
        private readonly IEnumerable<Assembly> _predefinedAssemblies;
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly DataContractJsonSerializer _serializer;

        private readonly List<CombinatorDescriptor> _combinators;
        private readonly List<SelectorDescriptor> _selectors;
        private readonly List<ModifierDescriptor> _pseudoSelectors;
        private readonly List<CommandDescriptor> _commandDescriptors;

        private enum DescriptorType
        {
            Selector, Combinator,
            PseudoSelector
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDescriptorRepository"/> class.
        /// </summary>
        public FileDescriptorRepository(String fileName, IEnumerable<Assembly> assemblies = null)
        {
            Assume.NotNull(fileName, "fileName");
            Assume.FileExists(fileName, "fileName");

            _serializer = new DataContractJsonSerializer(typeof(DescriptorModel));
            _combinators = new List<CombinatorDescriptor>();
            _selectors = new List<SelectorDescriptor>();
            _pseudoSelectors = new List<ModifierDescriptor>();
            _commandDescriptors = new List<CommandDescriptor>();
            _predefinedAssemblies = new[] { Assembly.Load("mscorlib") };

            _assemblies = assemblies ?? new[] { Assembly.GetExecutingAssembly() };

            loadFromFile(fileName);
        }

        private void loadFromFile(String fileName)
        {
            try
            {
                var reader = File.OpenRead(fileName);
                var descriptor = (DescriptorModel)_serializer.ReadObject(reader);

                _selectors.Clear();
                _combinators.Clear();
                _pseudoSelectors.Clear();
                _commandDescriptors.Clear();

                foreach (var commandDescriptor in descriptor.CommandDescriptors)
                {
                    var desc = new CommandDescriptor {
                        Name = commandDescriptor.Name,
                        Description = commandDescriptor.Description,
                        CommandNames = new List<String>(commandDescriptor.CommandNames)
                    };

                    desc.Arguments = commandDescriptor.Arguments.Select((ArgumentDescriptorModel arg) => new ArgumentDescriptor {
                        ArgumentName = arg.Name,
                        DefaultValue = arg.DefaultValue,
                        IsOptional = arg.IsOptional,
                        Position = arg.Position,
                        ArgumentType = findInAssemblies(arg.ArgumentType)
                    });

                    _commandDescriptors.Add(desc);
                }

                foreach (var selectionDescriptors in descriptor.SelectionDescriptors)
                {
                    switch (selectionDescriptors.SelectorType)
                    {
                        case "element-type-selector":
                            {
                                var type = findInAssemblies(selectionDescriptors.Type, DescriptorType.Selector);
                                _selectors.Add(new SelectorDescriptor(selectionDescriptors.Name, selectionDescriptors.Value, type));
                                break;
                            }
                        case "combinator":
                            {
                                var type = findInAssemblies(selectionDescriptors.Type, DescriptorType.Combinator);
                                _combinators.Add(new CombinatorDescriptor(selectionDescriptors.Name, selectionDescriptors.Value, type));
                                break;
                            }
                        case "pseudo-selector":
                            {
                                var type = findInAssemblies(selectionDescriptors.Type, DescriptorType.PseudoSelector);
                                _pseudoSelectors.Add(new ModifierDescriptor(selectionDescriptors.Name, selectionDescriptors.Value,
                                    selectionDescriptors.Arguments, type));
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
            var assemblies = _assemblies.Union(_predefinedAssemblies);

            var assemblyTypes = assemblies.SelectMany(assembly => assembly.GetTypes());
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
                    assignableFromType = typeof(CombinatorBase);
                    break;
                case DescriptorType.Selector:
                    assignableFromType = typeof(NodeSelectorBase);
                    break;
                case DescriptorType.PseudoSelector:
                    assignableFromType = typeof(NodeModifierBase);
                    break;
                default:
                    throw new NotSupportedException(String.Format("Not supported descriptor type: {0}", descriptorType));
            }

            var assemblyTypes = _assemblies.SelectMany(assembly => assembly.GetTypes());
            var descriptorTypes = assemblyTypes.Where(type => type != assignableFromType && assignableFromType.IsAssignableFrom(type));
            var matchedTypes = descriptorTypes.Where(type => type.FullName == typeName || type.Name == typeName);

            if (!matchedTypes.Any())
            {
                throw new Exception(String.Format("Not found descriptor type: {0}.", typeName));
            }

            var result = matchedTypes.SingleOrDefault();

            if (result == null)
            {
                throw new Exception("Ambiguation between descriptor types.");
            }

            return result;
        }

        /// <summary>
        /// Gets the combinators.
        /// </summary>
        public IEnumerable<CombinatorDescriptor> GetCombinators()
        {
            return _combinators.AsReadOnly();
        }

        /// <summary>
        /// Gets the pseudo selectors.
        /// </summary>
        public IEnumerable<ModifierDescriptor> GetPseudoSelectors()
        {
            return _pseudoSelectors.AsReadOnly();
        }

        /// <summary>
        /// Gets the selectors.
        /// </summary>
        public IEnumerable<SelectorDescriptor> GetSelectors()
        {
            return _selectors.AsReadOnly();
        }

        /// <summary>
        /// Gets the command descriptors.
        /// </summary>
        public IEnumerable<CommandDescriptor> GetCommandDescriptors()
        {
            return _commandDescriptors.AsReadOnly();
        }
    }
}