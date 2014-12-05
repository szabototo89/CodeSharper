using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ControlFlow;
using CodeSharper.Core.Utilities;
using Ninject;
using Ninject.Parameters;
using Ninject.Extensions.Conventions;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ControlFlowFactoryTestFixture : TestFixtureBase
    {
        [SetUp]
        public void Setup()
        {
            Kernel.Bind<SingleCommandCall>().ToSelf();
            Kernel.Bind(x => x.FromAssembliesMatching("*").SelectAllClasses().BindAllInterfaces());
        }

        [TearDown]
        public void Teardown()
        {
            this.Dispose();
        }

        [Test]
        public void StandardControlFlowFactoryShouldAbleToParseCommandCallAndBuildAnControlFlowFromIt()
        {
            // Given
            ICommandCall commandCall = Kernel.Get<SingleCommandCall>(new ConstructorArgument("descriptor", new CommandCallDescriptor("test")));
            var underTest = new StandardControlFlowFactory();

            // When
            var result = underTest.Parse(commandCall);

            // Then
            Assert.That(result, Is.Not.Null);

        }

    }
}
