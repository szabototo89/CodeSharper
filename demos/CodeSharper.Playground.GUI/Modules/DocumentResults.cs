using System;

namespace CodeSharper.Playground.GUI.Modules
{
    public struct DocumentResults
    {
        private String source;
        private String results;

        public DocumentResults(String source, String results)
        {
            this.source = source;
            this.results = results;
        }

        public String Source
        {
            get { return source; }
            set { source = value; }
        }

        public String Results
        {
            get { return results; }
            set { results = value; }
        }
    }
}