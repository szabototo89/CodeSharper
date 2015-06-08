using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Services;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Services
{
    [TestFixture]
    public class SerializerTests : TestFixtureBase
    {
        [Test(Description = "SelectionDescriptorModel should be deserializable when JSON serializer is used")]
        public void SelectorDescriptorModel_ShouldBeDeserializable_WhenJSONSerializeIsUsed()
        {
            // Given
            var input = "{\r\n    \"name\": \"relative-child-selector\",\r\n    \"selector-type\": \"combinator\",\r\n    \"value\": \"\",\r\n    \"type\": \"CodeSharper.Core.Nodes.Combinators.RelativeNodeCombinator\",\r\n}";

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.WriteLine(input);
            streamWriter.Flush();
            memoryStream.Position = 0;
            var serializer = new DataContractJsonSerializer(typeof(SelectionDescriptorModel));

            // When
            var result = serializer.ReadObject(memoryStream);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(new SelectionDescriptorModel {
                Name = "relative-child-selector",
                SelectorType = "combinator",
                Value = "",
                Type = "CodeSharper.Core.Nodes.Combinators.RelativeNodeCombinator"
            }));
        }

        [Test(Description = "SelectionDescriptorModel should be deserializable when JSON serializer is used")]
        public void SelectorDescriptorModel_ShouldBeSerializable_WhenJSONSerializeIsUsed()
        {
            // Given
            var input = new SelectionDescriptorModel {
                Name = "relative-child-selector",
                SelectorType = "combinator",
                Value = "",
                Type = "CodeSharper.Core.Nodes.Combinators.RelativeNodeCombinator"
            };

            var memoryStream = new MemoryStream();
            var streamReader = new StreamReader(memoryStream);
            var serializer = new DataContractJsonSerializer(typeof(SelectionDescriptorModel));

            // When
            serializer.WriteObject(memoryStream, input);
            memoryStream.Position = 0;
            var result = streamReader.ReadToEnd();

            // Then
            Assert.That(result, Is.Not.Null.And.Not.Empty);
            Assert.That(result, Is.EqualTo("{\"arguments\":null,\"name\":\"relative-child-selector\",\"selector-type\":\"combinator\",\"type\":\"CodeSharper.Core.Nodes.Combinators.RelativeNodeCombinator\",\"value\":\"\"}"));
        }

    }
}
