using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.TextTransformationTests
{
    internal class CommonTextTransformationsTests : TextTestFixtureBase
    {
        [Test]
        public void TextTransformationTestsShouldBeAbleToPass()
        {
            TestRunnableWithTestCasesOf(
              new ToUpperCaseRunnable(), TestCase(name: "touppercase")
                );

            TestRunnableWithTestCasesOf(
              new ToLowerCaseRunnable(), TestCase(name: "tolowercase")
            );
        }
    }
}
