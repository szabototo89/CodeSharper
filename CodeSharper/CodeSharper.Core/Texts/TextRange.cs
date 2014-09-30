using System;
using System.Linq;
using System.Reflection.Emit;
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

        private Int32 SpecifyStopIndex(Int32 start, String text)
        {
            return start + text.Length;
        }


        public String Text
        {
            get { return _text; }
        }

        public TextRange SetText(String value)
        {
            Constraints.NotNull(() => value);

            if (_text == value)
                return this;

            // TODO: update Stop property 
            TextDocument.UpdateText(this, value);

            _text = value;

            return this;
        }

        public TextDocument TextDocument { get; protected set; }

        /// <summary>
        /// Constructor for cloning of TextRange
        /// </summary>
        private TextRange() { }

        public TextRange(String text, TextDocument parent = null)
            : this(0, text, parent) { }

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

        public TextRange OffsetBy(int offset)
        {
            Constraints
                .IsGreaterThan(() => offset, 0);

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

        public override String ToString()
        {
            return String.Format("Start: {0} Stop: {1}", Start, Stop);
        }

    }
}
