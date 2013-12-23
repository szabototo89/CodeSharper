using CodeSharper.Common;

namespace CodeSharper.Csv
{
    public class CsvValueNode : CsvNode
    {
        public string Value { get; protected set; }

        public CsvValueNode(string text, TextPosition start, ICsvNode parent)
            : base(text, start, parent)
        {
            Initialize();
        }

        private void Initialize()
        {
            Value = Text.Trim();
        }
    }
}