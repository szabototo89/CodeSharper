using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;
using CodeSharper.Json;
using NUnit.Framework;

namespace CodeSharper.Tests.Json
{
    [TestFixture]
    class JsonLiteralExpressionTestFixture
    {
        [Test]
        public void JsonLiteralTest()
        {
            string text = "\"Hello World\"";
            var stringToken = new JsonStringToken(text, TextSpan.Zero);
            var expression = new JsonLiteralExpression(stringToken);
            Assert.IsTrue(expression.IsString);
            Assert.AreEqual(stringToken.TokenValue, text);

            var booleanToken = new JsonBooleanToken("false", TextSpan.Zero);
            expression = new JsonLiteralExpression(booleanToken);
            Assert.IsTrue(expression.IsBoolean);
            Assert.AreEqual(booleanToken.TokenValue, false);

            const int number = 352535;
            var numberToken = new JsonNumberToken(number.ToString(CultureInfo.InvariantCulture), TextSpan.Zero);
            expression = new JsonLiteralExpression(numberToken);
            Assert.IsTrue(expression.IsNumber);
            Assert.AreEqual(numberToken.TokenValue, number);
        }
    }
}
