using System.Runtime.Serialization;
using CodeSharper.Common;

namespace CodeSharper.Csv
{
    public class CsvCommaNode : CsvNode
    {
        public CsvCommaNode(TextPosition start, ICsvNode parent)
            : base(",", start, parent)
        {

        }
    }
}