using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Interfaces
{
    public interface IHasTextRange
    {
        /// <summary>
        /// Gets or sets the text range of child
        /// </summary>
        TextRange TextRange { get; }
    }
}