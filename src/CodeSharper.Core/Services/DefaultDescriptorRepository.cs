using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Selectors;

namespace CodeSharper.Core.Services
{
    public class DefaultDescriptorRepository : IDescriptorRepository
    {
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly DataContractJsonSerializer _serializer;

        private readonly List<CombinatorDescriptor> _combinators;
        private readonly List<SelectorDescriptor> _selectors;

        private enum DescriptorType { Selector, Combinator }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDescriptorRepository"/> class.
        /// </summary>
        public DefaultDescriptorRepository(String fileName, IEnumerable<Assembly> assemblies = null)
        {
            Assume.NotNull(fileName, "fileName");
            Assume.FileExists(fileName, "fileName");

            _serializer = new DataContractJsonSerializer(typeof(SelectionDescriptorModel[]));
            _combinators = new List<CombinatorDescriptor>();
            _selectors = new List<SelectorDescriptor>();

            _assemblies = assemblies ?? new[] { Assembly.GetExecutingAssembly() };

            loadFromFile(fileName);
        }

        private void loadFromFile(String fileName)
        {
            var reader = File.OpenRead(fileName);
            var descriptors = _serializer.ReadObject(reader) as SelectionDescriptorModel[];
            if (descriptors == null)
                throw new Exception(String.Format("Cannot parse specified file: {0}.", fileName));

            _selectors.Clear();
            _combinators.Clear();

            foreach (var descriptor in descriptors)
            {
                switch (descriptor.SelectorType)
                {
                    case "element-type-selector":
                    {
                        var type = findInAssemblies(descriptor.Type, DescriptorType.Selector);
                        _selectors.Add(new SelectorDescriptor(descriptor.Name, descriptor.Value, type));
                        break;
                    }
                    case "combinator":
                    {
                        var type = findInAssemblies(descriptor.Type, DescriptorType.Combinator);
                        _combinators.Add(new CombinatorDescriptor(descriptor.Name, descriptor.Value, type));
                        break;
                    }
                    case "pseudo-selector":
                        throw new NotImplementedException();
                        break;
                }
            }
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
                default:
                    throw new NotSupportedException(String.Format("Not supported descriptor type: {0}", descriptorType));
            }

            var assemblyTypes = _assemblies.SelectMany(assembly => assembly.GetTypes());
            var descriptorTypes = assemblyTypes; //.Where(type => type.IsAssignableFrom(assignableFromType));
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
        /// Loads the combinators.
        /// </summary>
        public IEnumerable<CombinatorDescriptor> LoadCombinators()
        {
            return _combinators.AsReadOnly();
        }

        /// <summary>
        /// Loads the selectors.
        /// </summary>
        public IEnumerable<SelectorDescriptor> LoadSelectors()
        {
            return _selectors.AsReadOnly();
        }
    }
}