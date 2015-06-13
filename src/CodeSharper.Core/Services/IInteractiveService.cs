using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Services
{
    public interface IInteractiveService
    {
        /// <summary>
        /// Transforms the specified parameters.
        /// </summary>
        IEnumerable<Object> Transform(IEnumerable<Object> parameters);
    }
}