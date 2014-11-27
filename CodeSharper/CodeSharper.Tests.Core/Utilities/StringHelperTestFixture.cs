using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.Mocks;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class StringHelperTestFixture
    {
        [Test]
        public void FormatMethodShouldAbleToFormatStringValues()
        {
            // When
            var result = "Hi {0}! I am here!".FormatString("User");

            // Then
            Assert.That(result, Is.EqualTo("Hi User! I am here!"));
        }

        [Test]
        public void ToEnumShouldAbleToParseStringToAnyKindOfEnum()
        {
            // Given
            var underTest = "Begin";

            // When
            var result = underTest.ToEnum<TypeMocks.TestEnum>();

            // Then
            Assert.That(result, Is.EqualTo(TypeMocks.TestEnum.Begin));
        }
    }
}
