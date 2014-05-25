using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core
{
    [TestFixture]
    class MutableNodeTestFixture
    {
        [Test(Description = "MutableNode should be initialized")]
        public void MutableNodeShouldBeInitializedTest()
        {
            // GIVEN
            var underTest = new MutableNode();

            // WHEN
            var result = new {
                Children = underTest.GetChildren(),
                Parent = underTest.GetParent()
            };

            // THEN
            Assert.That(result.Children, Is.Empty);
            Assert.That(result.Parent, Is.Null);
        }

        [Test(Description = "MutableNode should set parent.")]
        public void MutableNodeShouldSetParentTest()
        {
            // GIVEN
            var parent = new MutableNode();
            var underTest = new MutableNode();

            // WHEN
            underTest.SetParent(parent);
            var result = underTest.GetParent();
            
            // THEN
            Assert.That(result, Is.EqualTo(parent));
        }

        [Test(Description = "MutableNode should clear its children.")]
        public void MutableNodeShouldClearChildrenTest()
        {
            // GIVEN
            var underTest = new MutableNode();

            // WHEN
            underTest.ClearChildren();
            var result = underTest.GetChildren();

            // THEN
            Assert.That(result, Is.Empty);
        }

        [Test(Description = "MutableNode should get NodeTypeDescriptor.")]
        public void MutableNodeShouldGetNodeTypeDescriptorTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.GetNodeTypeDescriptor();

            // THEN
            Assert.That(result, Is.Not.Null);
        }

        [Test(Description = "AppendChild should add child to node.")]
        public void AppendChildShouldAddChildToNodeTest()
        {
            // GIVEN
            var child = new MutableNode();
            var underTest = new MutableNode();

            // WHEN
            var result = underTest.AppendChild(child);
            
            // THEN
            Assert.That(result.GetChildren(), Is.Not.Empty);
        }

        [Test(Description = "AppendChild should be chainable.")]
        public void AppendChildShouldBeChainableTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.AppendChild(new MutableNode())
                                  .AppendChild(new MutableNode());
            
            // THEN
            Assert.That(result, Is.EqualTo(underTest));
        }

        [Test(Description = "AppendChild should set parent of child")]
        public void AppendChildShouldSetParentOfChildTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = new MutableNode();
            underTest.AppendChild(result);
            
            // THEN
            Assert.That(result.GetParent(), Is.EqualTo(underTest));
        }

        [Test(Description = "SetParent should not pass itself")]
        public void SetParentShouldNotPassItselfTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            TestDelegate result = () => underTest.SetParent(underTest);

            // THEN
            Assert.That(result, Throws.ArgumentException);
        }

        [Test(Description = "SetParent should be chainable.")]
        public void SetParentShouldBeChainableTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.SetParent(new MutableNode());

            // THEN
            Assert.That(result, Is.EqualTo(underTest));
        }

        [Test(Description = "SetParent should throw exception when pass null")]
        public void SetParentShouldThrowExceptionWhenPassNullTest()
        {
            // GIVEN
            var underTest = new MutableNode();

            // WHEN
            TestDelegate result = () => underTest.SetParent(null);

            // THEN
            Assert.That(result, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test(Description = "ClearChildren should be chainable")]
        public void ClearChildrenShouldBeChainableTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.ClearChildren();

            // THEN
            Assert.That(result, Is.EqualTo(underTest));
        }

        [Test]
        public void MutableNodeShouldReturnTextInformationTest()
        {
            // GIVEN
            var underTest = new MutableNode();

            // WHEN
            var result = TextInformationHelper.GetTextInformation(underTest);

            // THEN
            Assert.That(result, Is.InstanceOf<TextInformation>());
        }
    }
}
