using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Trees;

namespace CodeSharper.Languages.Csv.Factories
{
    public interface ICsvTreeFactory
    {
        ICsvTreeFactory CreateRow(TextRange textRange);

        ICsvTreeFactory CreateField(TextRange textRange);

        ICsvTreeFactory CreateComma(TextRange textRange);

        ICsvTreeFactory CreateDocument(TextRange textRange);

        TextDocument GetTextDocument();

        SyntaxTree GetSyntaxTree();
    }
}
