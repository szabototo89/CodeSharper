using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;
using CodeSharper.Json;

namespace CodeSharper.Tests.Json
{
    internal class JsonEmptyNode : JsonNode
    {
        private JsonEmptyNode(string text, TextSpan span, JsonNode parent = null) 
            : base(text, span, parent)
        {
        }

        private static JsonEmptyNode _Instance;

        public static JsonEmptyNode Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new JsonEmptyNode(string.Empty, new TextSpan());

                return _Instance;
            }
        }
    }
}
