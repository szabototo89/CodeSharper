using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Constraints;
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
            var underTest = new NullReferenceConstraint(() => iAmNotNull);

            // When
            underTest.Check();

            // Then

        }
    }
}
