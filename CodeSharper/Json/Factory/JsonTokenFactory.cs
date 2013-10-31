using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;

namespace CodeSharper.Json.Factory
{
    public class JsonTokenFactory
    {
        public IJsonNode Parent { get; protected set; }

        public JsonTokenFactory(IJsonNode parent)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            Parent = parent;
        }

        public IJsonStringToken CreateStringToken(string text, TextSpan span)
        {
            return new JsonStringToken(text, span, Parent);
        }

        public IJsonNumberToken CreateNumberToken(string text, TextSpan span)
        {
            return new JsonNumberToken(text, span, Parent);
        }

        public IJsonBooleanToken CreateBooleanToken(string text, TextSpan span)
        {
            return new JsonBooleanToken(text, span, Parent);
        }
    }
}
