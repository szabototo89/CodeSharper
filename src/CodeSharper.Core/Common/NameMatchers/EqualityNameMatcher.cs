using System;

namespace CodeSharper.Core.Common.NameMatchers
{
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