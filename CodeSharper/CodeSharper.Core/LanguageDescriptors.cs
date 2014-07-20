using System;

namespace CodeSharper.Core
{
    /// <summary>
    /// Descriptor of language
    /// </summary>
    public static class LanguageDescriptors
    {
        private static LanguageDescriptor _any;

        private static LanguageDescriptor _csv;

        public static LanguageDescriptor Any
        {
            get
            {
                return _any ?? (_any =
                    new LanguageDescriptor(String.Empty, new[] { ProgrammingParadigm.None }));
            }
        }

        public static LanguageDescriptor Csv
        {
            get
            {
                return _csv ?? (_csv =
                    new LanguageDescriptor("Comma-Separated Values (CSV)", new[] { ProgrammingParadigm.Declarative }));
            }
        }

    }
}