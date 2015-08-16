using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Languages.CSharp.Selectors.ClassSelectors
{
    public interface IClassSelector
    {
        /// <summary>
        /// Filters the specified identifier by the specified class selectors. It returns true if matches to any of class selector.
        /// </summary>
        Boolean Filter(IEnumerable<Regex> classSelectors, SyntaxToken token);
    }
}