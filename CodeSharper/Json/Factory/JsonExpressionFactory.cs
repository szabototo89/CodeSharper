using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;

namespace CodeSharper.Json.Factory
{
    public class JsonExpressionFactory
    {
        protected IJsonExpression Expression { get; set; }

        public IJsonNode Parent { get; protected set; }

        public JsonExpressionFactory(IJsonNode parent)
        {
            Parent = parent;
            Expression = new JsonExpression(null, parent);
        }

        public JsonExpressionFactory AddKeyValuePair(IJsonId key, IJsonBaseExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
