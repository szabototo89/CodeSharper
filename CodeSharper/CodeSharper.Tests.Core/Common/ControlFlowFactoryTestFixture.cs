using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ControlFlowFactoryTestFixture
    {
        [SetUp]
        public void Setup() { }

        [TearDown]
        public void Teardown() { }

        [Test]
        public void StandardControlFlowFactoryShouldAbleToParseCommandCallAndBuildAnControlFlowFromIt()
        {
            // Given
            ICommandCall commandCall;
            var underTest = new StandardControlFlowFactory();

            // When
            var result = underTest.Parse(commandCall);

            // Then

        }

    }
}
