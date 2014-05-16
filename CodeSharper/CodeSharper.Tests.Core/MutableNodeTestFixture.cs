using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;
using NUnit.Framework;

namespace CodeSharper.Tests.Core
{
    [TestFixture]
    class MutableNodeTestFixture
    {
        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void MutableNodeShouldGetNodeTypeDescriptorTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.GetNodeTypeDescriptor();

            // THEN
            Assert.That(result, Is.Not.Null);
        }

        [Test]
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

        [Test]
        public void ApppendChildShouldBeChainableTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.AppendChild(new MutableNode())
                                  .AppendChild(new MutableNode());
            
            // THEN
            Assert.That(result, Is.EqualTo(underTest));
        }

        [Test]
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

        [Test]
        public void SetParentShouldNotPassItselfTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            TestDelegate result = () => underTest.SetParent(underTest);

            // THEN
            Assert.That(result, Throws.ArgumentException);
        }

        [Test]
        public void SetParentShouldBeChainableTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.SetParent(new MutableNode());

            // THEN
            Assert.That(result, Is.EqualTo(underTest));
        }

        [Test]
        public void SetParentShouldThrowExceptionWhenPassNullTest()
        {
            // GIVEN
            var underTest = new MutableNode();

            // WHEN
            TestDelegate result = () => underTest.SetParent(null);

            // THEN
            Assert.That(result, Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void ClearChildrenShouldBeChainableTest()
        {
            // GIVEN
            var underTest = new MutableNode();
            
            // WHEN
            var result = underTest.ClearChildren();

            // THEN
            Assert.That(result, Is.EqualTo(underTest));
        }
    }
}
