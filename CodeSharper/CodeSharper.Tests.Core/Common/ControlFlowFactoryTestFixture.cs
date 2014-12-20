using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.ControlFlow;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.TestHelpers;
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
            /*Kernel.Bind(
                x => x.FromAssembliesMatching("*")
                      .SelectAllClasses()
                      .Excluding<ICommandManager>()
                      .BindAllInterfaces());*/

            Kernel.Bind<ICommandManager>().To<StandardCommandManager>();
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
            ICommandCall commandCall = Kernel.Get<SingleCommandCall>(
                new ConstructorArgument("descriptor", new CommandCallDescriptor("test")));

            var commandManager = Kernel.Get<ICommandManager>();

            var underTest = new StandardControlFlowFactory(commandManager);

            // When
            var result = underTest.CreateControlFlow(commandCall);

            // Then
            Assert.That(result, Is.Not.Null);

        }

    }
}
