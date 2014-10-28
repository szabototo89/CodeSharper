using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    [DebuggerDisplay("TextRange({Text})")]
    public class TextRange : IDisposable
    {
        private readonly List<TextRange> _children;

        public Int32 Start { get; protected set; }

        public Int32 Stop { get; protected set; }

        public Int32 Length { get; protected set; }

        public TextRange Parent { get; protected set; }

        public TextDocument TextDocument { get; set; }

        public virtual String Text { get { return TextDocument.Text.ToString(Start, Length); } }

        public IEnumerable<TextRange> Children { get { return _children; } }

        public TextRange(Int32 start, Int32 stop, TextDocument document)
        {
            Constraints.NotNull(() => document);

            _children = new List<TextRange>();
            Start = start;
            Stop = stop;
            Length = stop - start;
            TextDocument = document;
            TextDocument.Register(this);
        }

        void IDisposable.Dispose()
        {
            TextDocument.Unregister(this);
        }

        public TextRange SubStringOfText(Int32 start, Int32 length)
        {
            var child = new TextRange(start, start + length, TextDocument)
            {
                Parent = this
            };

            _children.Add(child);

            return child;
        }

        public TextRange ReplaceText(String value)
        {
            Constraints
                .NotNull(() => value);

            var offsetLength = value.Length - Length;
            TextDocument.ReplaceText(this, value);

            if (offsetLength != 0)
            {
                UpdateTextByLength(value.Length);
                UpdateParentsTextLength(offsetLength);
            }

            return this;
        }

        private void UpdateParentsTextLength(Int32 offsetLength)
        {
            TextRange parent = Parent;
            while (parent != null)
            {
                parent.UpdateTextByLength(parent.Length + offsetLength);
                parent = parent.Parent;
            }
        }

        private void UpdateTextByLength(Int32 length)
        {
            Length = length;
            Stop = Start + length;
        }

        public TextRange OffsetBy(Int32 value)
        {
            Constraints
                .Evaluate(() => value, _ => Start + value >= 0, "Start must be positive!");
            Constraints
                .Evaluate(() => value, _ => Stop + value >= 0, "Stop must be positive!");

            Start += value;
            Stop += value;
            return this;
        }
    }
}
