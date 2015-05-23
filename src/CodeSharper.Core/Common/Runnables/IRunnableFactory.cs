using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Runtime.Remoting;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnableFactory
    {
        /// <summary>
        /// Creates a runnable with the specified name and actual arguments
        /// </summary>
        IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments);
    }
}