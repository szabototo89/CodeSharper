using System.Collections.Generic;
using System.Collections.Specialized;
using CodeSharper.Core.Csv;

namespace CodeSharper.Core
{
    /// <summary>
    /// Represents programming paradigm
    /// </summary>
    public enum ProgrammingParadigm
    {
        Functional, Imperative, ObjectOrientated, Declarative, MultiParadigm, None
    }

    /// <summary>
    /// Represents a descriptor of programming language.
    /// </summary>
    public class LanguageDescriptor
    {
        /// <summary>
        /// Gets or sets the supported programming paradigms.
        /// </summary>
        public IEnumerable<ProgrammingParadigm> SupportedProgrammingParadigms { get; protected set; }

        /// <summary>
        /// Gets the name of language.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageDescriptor"/> class.
        /// </summary>
        public LanguageDescriptor(string name, IEnumerable<ProgrammingParadigm> programmingParadigms)
        {
            Name = name;
            SupportedProgrammingParadigms = programmingParadigms;
        }
    }
}