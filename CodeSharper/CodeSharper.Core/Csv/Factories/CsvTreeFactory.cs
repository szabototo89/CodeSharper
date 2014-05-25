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
    public interface ICsvNodeFactory
    {
        FieldNode Field(string value);
        RecordNode Record(IEnumerable<FieldNode> fields);
        CsvCompilationUnit CompilationUnit(RecordNode[] records);
        CommaNode Comma();
    }

    public class CsvTreeFactory : ICsvNodeFactory
    {
        private readonly CsvAbstractSyntaxTree _ast;
        private readonly Stack<TextSpan> _spans;

        public CsvTreeFactory(CsvAbstractSyntaxTree ast)
        {
            if (ast == null)
                throw ThrowHelper.ArgumentNullException("ast");

            _ast = ast;
            _spans = new Stack<TextSpan>();
        }

        private TNode RegisterNodeToTextInformationManager<TNode>(TNode node)
            where TNode : CsvMutableNode
        {
            var span = new TextSpan();
            if (_spans.Count > 0)
                span = _spans.Pop();

            _ast.TextInformationManager.RegisterNode(node, span);
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

        public RecordNode Record(IEnumerable<FieldNode> fields)
        {
            if (fields == null)
                throw ThrowHelper.ArgumentNullException("fields");

            var node = new RecordNode(fields);
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public CsvCompilationUnit CompilationUnit(RecordNode[] records)
        {
            var node = new CsvCompilationUnit(records);
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public CommaNode Comma()
        {
            var node = new CommaNode();
            RegisterNodeToTextInformationManager(node);
            return node;
        }

        public ICsvNodeFactory UpdateTextSpan(TextSpan span)
        {
            _spans.Push(span);
            return this;
        }
    }
}
