using System;

namespace CodeSharper.Core.Common.NameMatchers
{
    public interface INameMatcher
    {
        /// <summary>
        /// Matches the specified collection.
        /// </summary>
        Boolean Match(String expected, String actual);
    }
}