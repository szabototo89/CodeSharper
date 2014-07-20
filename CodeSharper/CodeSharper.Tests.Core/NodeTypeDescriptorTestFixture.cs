using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using NUnit.Framework;

namespace CodeSharper.Tests.Core
{
    [TestFixture]
    public class NodeTypeDescriptorTestFixture
    {
        [Test(Description = "NodeTypeDescriptor should get language descriptor.")]
        public void NodeTypeDescriptorShouldGetLanguageTest()
        {
            // GIVEN
            var underTest = new NodeTypeDescriptor();
            
            // WHEN
            var result = underTest.Language;
            
            // THEN
            Assert.That(result, Is.InstanceOf<LanguageDescriptor>());
            Assert.That(result, Is.EqualTo(LanguageDescriptors.Any));
        }

        [Test(Description = "NodeTypeDescriptor should get type.")]
        public void NodeTypeDescriptorShouldGetTypeTest()
        {
            // GIVEN
            var underTest = new NodeTypeDescriptor();

            // WHEN
            var result = underTest.Type;

            // THEN
            Assert.That(result, Is.InstanceOf<NodeType>());
            Assert.That(result, Is.EqualTo(NodeType.Undefined));
        }
    }
}
