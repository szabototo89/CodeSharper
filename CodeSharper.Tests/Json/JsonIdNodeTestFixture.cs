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
    class JsonIdNodeTestFixture
    {
        private void JsonTypeTest(string value, JsonIdType type)
        {
            Assert.DoesNotThrow(() => {
                var jsonId = new JsonId(value, new TextSpan(), JsonEmptyNode.Instance);
                Assert.AreEqual(jsonId.IdType, type);
            });
        }

        [Test]
        public void JsonNumberTypeTest()
        {
            JsonTypeTest("24", JsonIdType.Integer);
        }
        
        [Test]
        public void JsonIdentifierTypeTest()
        {
            JsonTypeTest("numberId", JsonIdType.Identifier);
        }
        
        [Test]
        public void JsonStringTypeTest()
        {
            JsonTypeTest("\"numberId\"", JsonIdType.String);
            JsonTypeTest("'numberId'", JsonIdType.String);
        }
    }
}
