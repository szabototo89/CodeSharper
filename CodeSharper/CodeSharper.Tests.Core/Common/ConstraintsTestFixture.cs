using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintUtils;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    class ConstraintsTestFixture
    {
        [Test]
        public void ConstraintsShouldCheckNullReferenceOfAParameter()
        {
            // Given
            var iAmNotNull = 10;
            var underTest = new NotNullConstraint(() => iAmNotNull);

            // When
            TestDelegate func = () => underTest.Check();

            // Then
            Assert.That(func, Throws.Nothing);
        }

        [Test]
        public void ConstraintsShouldAbleToUseViaConstraintsStaticClass()
        {
            // Given
            var iAmNotNull = 10;
            TestDelegate underTest = () => Constraints.NotNull(() => iAmNotNull);

            // Then
            Assert.That(underTest, Throws.Nothing);
        }

        [Test]
        public void ConstraintsShouldContainNotBlankRestriction()
        {
            // Given
            string value = "Hello World!";
            TestDelegate underTest = () => Constraints.NotBlank(() => value);

            // Then
            Assert.That(underTest, Throws.Nothing);
        }


    }
}
