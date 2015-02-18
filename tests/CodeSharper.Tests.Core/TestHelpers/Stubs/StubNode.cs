using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Tests.Core.TestHelpers.Stubs
{
    internal class StubNode : Node, IEquatable<StubNode>
    {
        private readonly static TextDocument _textDocument = new TextDocument(String.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="StubNode"/> class.
        /// </summary>
        public StubNode(String text)
            : base(new TextRange(0, 0, _textDocument))
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the text of node stub
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as StubNode);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (Text != null ? Text.GetHashCode() : 0);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(StubNode other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return String.Equals(Text, other.Text);
        }
    }
}
