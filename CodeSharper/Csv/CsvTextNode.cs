using System.Collections.Generic;
using System.Linq;
using CodeSharper.Common;

namespace CodeSharper.Csv
{
    public class CsvTextNode : CsvNode
    {
        public CsvTextNode(string text)
            : base(text, TextPosition.Zero, null)
        {
            Parse(text);
        }

        public IEnumerable<CsvValueNode> Values { get; private set; }

        public void Parse(string text)
        {
            _Children.Clear();

            var isString = false;
            var value = "";
            var lastPosition = Span.Start;

            foreach (var character in text) {
                if (character == '"')
                    isString = !isString;
                else if (character == ',' && !isString) {
                    _Children.Add(new CsvValueNode(value, lastPosition, this));
                    lastPosition = lastPosition.OffsetByString(value);
                    _Children.Add(new CsvCommaNode(lastPosition, this));
                    lastPosition = lastPosition.OffsetByCharPosition(1);
                    value = string.Empty;
                }
                else
                    value += character;
            }

            if (!string.IsNullOrEmpty(value)) {
                _Children.Add(new CsvValueNode(value, lastPosition, this));
            }

            Values = _Children.OfType<CsvValueNode>().ToArray();
        }
    }
}