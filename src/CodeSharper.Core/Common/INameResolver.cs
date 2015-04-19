using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Common
{
    public interface INameResolver
    {
        /// <summary>
        /// Matches the specified collection.
        /// </summary>
        Boolean Match(IEnumerable<String> collection, String value);
    }
}