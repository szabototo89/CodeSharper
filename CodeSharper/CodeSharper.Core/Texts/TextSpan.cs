using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public struct TextSpan
    {
        #region Private fields

        private readonly int _start;
        private readonly int _stop;
        private readonly string _text;

        #endregion

        #region Private methods

        private int SpecifyStopIndex(int start, string text)
        {
            return start + text.Length;
        }

        #endregion

        #region Constructors

        public TextSpan(string text)
            : this(0, text)
        {
        }

        public TextSpan(int start, string text) : this()
        {
            if (start < 0)
                throw ThrowHelper.ArgumentException("start", "Start is negative number");

            Constraints
                .Argument(() => text)
                    .NotBlank();

            _start = start;
            _text = text;
            _stop = SpecifyStopIndex(start, text);
        }

        #endregion

        #region Public properties

        public string Text
        {
            get { return _text; }
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

        #endregion

        #region Public methods

        public override string ToString()
        {
            return String.Format("Start: {0} Stop: {1}", Start, Stop);
        }

        #endregion
    }
}