using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Csv.Factories
{
    public class CsvTreeFactory : ICsvNodeFactory
    {
        #region Private fields

        private readonly CsvAbstractSyntaxTree _ast;
        private readonly Stack<TextSpan> _spans;

        #endregion

        #region Constructors

        public CsvTreeFactory(CsvAbstractSyntaxTree ast)
        {
            if (ast == null)
                throw ThrowHelper.ArgumentNullException("ast");

            _ast = ast;
            _spans = new Stack<TextSpan>();
        }

        #endregion

        #region Private methods

        private TNode RegisterNodeToTextInformationManager<TNode>(TNode node)
            where TNode : CsvMutableNode
        {
            var span = new TextSpan();
            if (_spans.Count > 0)
                span = _spans.Pop();

            _ast.TextInformationManager.RegisterNode(node, span);
            return node;
        }

        #endregion

        #region Public methods

        public CommaNode Comma()
        {
            var node = new CommaNode();
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public CsvCompilationUnit CompilationUnit(RecordNode[] records)
        {
            var node = new CsvCompilationUnit(records);
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public FieldNode Field(string value)
        {
            if (value == null)
                throw ThrowHelper.ArgumentNullException("value");

            var node = new FieldNode() { Value = value };
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public RecordNode Record(IEnumerable<FieldNode> fields, IEnumerable<DelimiterNode> delimiters)
        {
            if (fields == null)
                throw ThrowHelper.ArgumentNullException("fields");

            if (delimiters == null)
                throw ThrowHelper.ArgumentNullException("delimiters");

            var node = new RecordNode(fields, delimiters);
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public ICsvNodeFactory UpdateTextSpan(TextSpan span)
        {
            _spans.Push(span);
            return this;
        }

        #endregion
    }
}
