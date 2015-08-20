using System;
using CodeSharper.Core.Services;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Services
{
    [TestFixture]
    public class SimpleServiceFactoryTests
    {
        public class Initialize : TestFixtureBase
        {
            protected SimpleServiceFactory underTest;

            public override void Setup()
            {
                base.Setup();

                underTest = new SimpleServiceFactory();
            }
        }

        public class DummyService
        {
        }

        public class RegisterServiceMethod : Initialize
        {
            [Test(Description = "should throw ArgumentNullException when service parameter is null")]
            public void ShouldThrowArgumentNullException_WhenServiceParameterIsNull()
            {
                // Arrange + Act
                TestDelegate callingRegisterServiceMethod = () => underTest.RegisterService<Object>(null);

                // Assert
                Assert.That(callingRegisterServiceMethod, Throws.TypeOf<ArgumentNullException>());
            }

            [Test(Description = "should throw ArgumentException when multiple services have been added with same types")]
            public void ShouldThrowArgumentException_WhenMultipleServicesHaveBeenAddedWithSameTypes()
            {
                // Arrange + Act
                TestDelegate registerMultipleServices = () =>
                {
                    underTest.RegisterService(new DummyService());
                    underTest.RegisterService(new DummyService());
                };

                // Assert
                Assert.That(registerMultipleServices, Throws.ArgumentException);
            }
        }

        public class GetServiceMethod : Initialize
        {
            [Test(Description = "should return same service object after registering it")]
            public void ShouldReturnSameServiceObject_AfterRegisteringIt()
            {
                // Arrange
                var service = new DummyService();
                underTest.RegisterService(service);

                // Act
                var result = underTest.GetService<DummyService>();

                // Assert
                Assert.That(result, Is.SameAs(service));
            }

            [Test(Description = "should throw ArgumentException when service type is not registered")]
            public void ShouldThrowArgumentException_WhenServiceTypeIsNotRegistered()
            {
                // Arrange + Act
                TestDelegate gettingInvalidService = () => underTest.GetService<DummyService>();

                // Assert
                Assert.That(gettingInvalidService, Throws.TypeOf<ArgumentException>());
            }
        }
    }
}