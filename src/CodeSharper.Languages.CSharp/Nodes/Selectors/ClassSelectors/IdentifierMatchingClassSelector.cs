using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Languages.CSharp.Nodes.Selectors.ClassSelectors
{
    public class IdentifierMatchingClassSelector : IClassSelector
    {
        /// <summary>
        /// Filters the specified identifier by the specified class selectors. It returns true if matches to any of class selector.
        /// </summary>
        public Boolean Filter(IEnumerable<Regex> classSelectors, SyntaxToken identifier)
        {
            return classSelectors.Any(selector => selector.IsMatch(identifier.ValueText));
        }
    }
}