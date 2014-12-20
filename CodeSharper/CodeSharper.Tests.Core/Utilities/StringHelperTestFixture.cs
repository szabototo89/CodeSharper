using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class StringHelperTestFixture : TestFixtureBase
    {
        [Test]
        public void FormatMethodShouldAbleToFormatStringValues()
        {
            // When
            var result = "Hi {0}! I am here!".FormatString("User");

            // Then
            Assert.That(result, Is.EqualTo("Hi User! I am here!"));
        }
    }
}
