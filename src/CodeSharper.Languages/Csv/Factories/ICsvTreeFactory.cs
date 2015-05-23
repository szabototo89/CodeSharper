using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Trees;

namespace CodeSharper.Languages.Csv.Factories
{
    public interface ICsvTreeFactory : IHasSyntaxTree
    {
        /// <summary>
        /// Creates a row of CSV
        /// </summary>
        ICsvTreeFactory CreateRow(TextRange textRange);

        /// <summary>
        /// Creates a field of CSV
        /// </summary>
        ICsvTreeFactory CreateField(TextRange textRange);

        /// <summary>
        /// Creates a comma of CSV.
        /// </summary>
        ICsvTreeFactory CreateComma(TextRange textRange);

        /// <summary>
        /// Creates a CSV document.
        /// </summary>
        ICsvTreeFactory CreateDocument(TextRange textRange);

        /// <summary>
        /// Gets the text document.
        /// </summary>
        TextDocument GetTextDocument();
    }
}
