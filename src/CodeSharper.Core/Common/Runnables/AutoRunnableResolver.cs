using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeSharper.Core.Common.Runnables
{
    public class AutoRunnableResolver
    {
        /// <summary>
        /// Resolves the runnable types from specified assemblies.
        /// </summary>
        public IEnumerable<Type> ResolveRunnableTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.ExportedTypes)
                             .Where(type => typeof(IRunnable).IsAssignableFrom(type))
                             .ToList().AsReadOnly();
        }
    }
}