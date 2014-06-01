using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Csv.Nodes;

namespace CodeSharper.Core.Texts
{
    public class TextInformationManager
    {
        private readonly Dictionary<MutableNode, TextSpan> _NodesByTextSpan;

        public TextInformationManager()
        {
            _NodesByTextSpan = new Dictionary<MutableNode, TextSpan>();
        }

        public void RegisterNode(MutableNode node, TextSpan span)
        {
            if (node == null)
                throw ThrowHelper.ArgumentNullException("node");

            _NodesByTextSpan.Add(node, span);
        }

        public IEnumerable<MutableNode> GetNodesByTextLocation(int location)
        {
            return _NodesByTextSpan.Select(pair => new { Node = pair.Key, Span = pair.Value })
                                   .Where(value => value.Span.Start <= location &&
                                                   location <= value.Span.Stop)
                                   .Select(value => value.Node)
                                   .ToArray();
        }
    }
}
