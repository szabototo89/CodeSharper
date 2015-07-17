using System;

namespace CodeSharper.Playground.GUI.Modules
{
    public struct DocumentResults
    {
        private String source;
        private String results;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentResults"/> struct.
        /// </summary>
        public DocumentResults(String source, String results)
        {
            this.source = source;
            this.results = results;
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public String Source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        public String Results
        {
            get { return results; }
            set { results = value; }
        }
    }
}