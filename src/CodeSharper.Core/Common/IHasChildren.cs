using System.Collections.Generic;

namespace CodeSharper.Core.Common
{
    /// <summary>
    /// Represents an object which may have children
    /// </summary>
    public interface IHasChildren<out TChild>
    {
        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        IEnumerable<TChild> Children { get; }
    }
}