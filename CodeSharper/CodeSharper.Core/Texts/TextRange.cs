using System;
using System.Linq;
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

        private readonly Int32 _start;
        private readonly Int32 _stop;

        private int SpecifyStopIndex(int start, string text)
        {
            return start + text.Length;
        }


        public String Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;

                _text = value;
                TextDocument.UpdateText(this, value);
            }
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

        public int Start
        {
            get { return _start; }
        }

        public int Stop
        {
            get { return _stop; }
        }

        public int Length
        {
            get { return Text.Length; }
        }

        public override string ToString()
        {
            return String.Format("Start: {0} Stop: {1}", Start, Stop);
        }

    }
}
