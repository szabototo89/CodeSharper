using System;
using System.Collections.Generic;

namespace CodeSharper.Core.Common
{
    public interface INameMatcher
    {
        /// <summary>
        /// Matches the specified collection.
        /// </summary>
        Boolean Match(String expected, String actual);
    }

    public class EqualityNameMatcher : INameMatcher
    {
        /// <summary>
        /// Matches the specified collection.
        /// </summary>
        public Boolean Match(String expected, String actual)
        {
            return expected == actual;
        }
    }
}