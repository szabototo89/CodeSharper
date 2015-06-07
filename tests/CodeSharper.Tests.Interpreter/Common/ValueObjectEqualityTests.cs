using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Interpreter.Common;
using NUnit.Framework;

namespace CodeSharper.Tests.Interpreter.Common
{
    [TestFixture]
    public class ValueObjectEqualityTests : TestFixtureBase
    {
        [Test(Description = "ConstantElement should be value object")]
        public void ConstantElement_ShouldBeValueObject()
        {
            // Given
            var underTest = new ConstantElement(10, typeof (Int32));

            // Then
            AssertHelper.AreEqualByValue(underTest, new ConstantElement(10, typeof (Int32)));
            AssertHelper.AreNotEqualByValue(underTest, new ConstantElement(11, typeof (Int32)));
            AssertHelper.AreNotEqualByValue(underTest, new ConstantElement(10, typeof (Double)));
        }

        [Test(Description = "ElementTypeSelector should be value object")]
        public void ElementTypeSelector_ShouldBeValueObject()
        {
            // Given
            var underTest = new ElementTypeSelector
            {
                Name = "test",
                Attributes = Enumerable.Empty<AttributeElement>(),
                PseudoSelectors = Enumerable.Empty<PseudoSelectorElement>()
            };

            // Then
            AssertHelper.AreEqualByValue(underTest, new ElementTypeSelector
            {
                Name = "test",
                Attributes = Enumerable.Empty<AttributeElement>(),
                PseudoSelectors = Enumerable.Empty<PseudoSelectorElement>()
            });
            AssertHelper.AreEqualByValue(new ElementTypeSelector(), new ElementTypeSelector());
            AssertHelper.AreNotEqualByValue(underTest, new ElementTypeSelector());
        }

        [Test(Description = "ActualParameterElement should be value object")]
        public void ActualParameterElement_ShouldBeValueObject()
        {
            // Given
            var value = new ConstantElement(10, typeof (Int32));
            var underTest = new ActualParameterElement(value, position: 0);

            // Then
            AssertHelper.AreEqualByValue(underTest, new ActualParameterElement(value, position: 0));
        }
    }
}