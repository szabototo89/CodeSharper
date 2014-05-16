using System.Collections.Specialized;

namespace CodeSharper.Core
{
    /// <summary>
    /// Represents a descriptor of programming language.
    /// </summary>
    public class LanguageDescriptor
    {
        /// <summary>
        /// Represents all languages
        /// </summary>
        public static readonly LanguageDescriptor Any;

        /// <summary>
        /// Gets the name of language.
        /// </summary>
        public virtual string Name { get { return string.Empty; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageDescriptor"/> class.
        /// </summary>
        protected LanguageDescriptor() { }

        /// <summary>
        /// Initializes the <see cref="LanguageDescriptor"/> class.
        /// </summary>
        static LanguageDescriptor()
        {
            Any = new LanguageDescriptor();
        }
    }
}