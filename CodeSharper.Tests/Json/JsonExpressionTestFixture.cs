using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;
using CodeSharper.Json;
using NUnit.Framework;

namespace CodeSharper.Tests.Json
{
    [TestFixture]
    class JsonExpressionTestFixture
    {
        [Test]
        public void JsonNullExpressionTest()
        {
            Assert.Throws<Exception>(() => {
                var expression = new JsonExpression(null);
            });
        }

        [Test]
        public void JsonEmptyExpressionTest()
        {
            Assert.DoesNotThrow(() => {
                var expression = new JsonExpression(new JsonNode[]
                {
                    new JsonLeftBracket(JsonBracketType.Curly, "{", TextSpan.Zero), 
                    new JsonExpressionItem(new JsonId(JsonIdType.Identifier, "apple", TextSpan.Zero, JsonEmptyNode.Instance), 
                                           new JsonSeparator(" :  ", TextSpan.Zero),
                                           new JsonLiteralExpression(new JsonNumberToken("34", TextSpan.Zero))),
                    new JsonComma(", ", TextSpan.Zero), 
                    new JsonExpressionItem(new JsonId(JsonIdType.Identifier, "apple", TextSpan.Zero, JsonEmptyNode.Instance), 
                                           new JsonSeparator(" :  ", TextSpan.Zero),
                                           new JsonLiteralExpression(new JsonNumberToken("34", TextSpan.Zero))),
                    new JsonRightBracket(JsonBracketType.Curly, "}", TextSpan.Zero),            
                });

                Console.WriteLine(expression);
            });

            Assert.Throws<Exception>(() => {
                var expression = new JsonExpression(
                    new JsonTokenNode[]
                    {
                        new JsonLeftBracket(JsonBracketType.Curly, "{", TextSpan.Zero),
                        new JsonRightBracket(JsonBracketType.Square, "]", TextSpan.Zero), 
                    }
                );
            });
        }
    }
}
