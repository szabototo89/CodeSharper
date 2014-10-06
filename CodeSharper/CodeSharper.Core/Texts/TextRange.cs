using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextRange
    {
        private String _text;

        private Int32 _start;
        private Int32 _stop;

        private List<TextRange> _children;

        private Int32 SpecifyStopIndex(Int32 start, String text)
        {
            return start + text.Length;
        }

        public String Text
        {
            get { return _text; }
        }

        public TextRange ReplaceText(String value)
        {
            Constraints.NotNull(() => value);

            if (_text == value)
                return this;

            TextDocument.UpdateTextByRange(this, value);
            _text = value;

            return this;
        }

        public TextDocument TextDocument { get; protected set; }

        /// <summary>
        /// Constructor for cloning of TextRange
        /// </summary>
        private TextRange()
        {
            _children = new List<TextRange>();
            Parent = TextRange.Empty;
        }

        public static readonly TextRange Empty = new TextRange();

        private TextRange(Int32 start, String text, TextDocument textDocument, TextRange parent)
            : this(start, text, textDocument)
        {
            Parent = parent;
        }

        public TextRange(String text, TextDocument textDocument = null)
            : this(0, text, textDocument) { }

        public TextRange(Int32 start, String text, TextDocument textDocument = null)
            : this()
        {
            Constraints.NotNull(() => text);

            _text = text;
            TextDocument = textDocument;

            if (start < 0)
                throw ThrowHelper.ArgumentException("start", "Start is negative number");

            _start = start;
            _text = text;
            _stop = SpecifyStopIndex(start, text);

        }

        public TextRange OffsetBy(Int32 offset)
        {
            if (Start + offset < 0)
                throw ThrowHelper.InvalidOperationException();

            _start = _start + offset;
            _stop = _stop + offset;

            return this;
        }

        public Int32 Start
        {
            get { return _start; }
        }

        public Int32 Stop
        {
            get { return _stop; }
        }

        public Int32 Length
        {
            get { return Text.Length; }
        }

        public IEnumerable<TextRange> Children { get { return _children; } }
        public TextRange Parent { get; private set; }

        public override String ToString()
        {
            return String.Format("Start: {0} Stop: {1}", Start, Stop);
        }

        public TextRange SubStringOfText(Int32 start, Int32 exclusiveStop)
        {
            Constraints
                .IsLesserThan(() => exclusiveStop, start + Text.Length);

            var node = new TextRange(start, Text.Substring(start, exclusiveStop - start), TextDocument, this);
            AppendChild(node);
            return node;
        }

        protected TextRange AppendChild(TextRange child)
        {
            Constraints
                .NotNull(() => child);

            _children.Add(child);

            return this;
        }

    }
}
