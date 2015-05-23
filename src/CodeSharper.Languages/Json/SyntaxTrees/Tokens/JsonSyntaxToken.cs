using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Tokens
{
    public class JsonSyntaxToken : JsonNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSyntaxToken"/> class.
        /// </summary>
        public JsonSyntaxToken(TextRange textRange)
            : base(textRange)
        {
        }
    }
}
