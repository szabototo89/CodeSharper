using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Services;
using NUnit.Framework;
using static CodeSharper.Core.Utilities.ConstructsHelper;

namespace CodeSharper.Tests.Core.Services
{
    [TestFixture]
    public class AutoCommandDescriptorRepositoryTests : TestFixtureBase
    {
        [CommandDescriptor("test", Description = "This is a description")]
        internal class TestRunnable : IRunnable
        {
            [Parameter("my-parameter")]
            public String MyParameter { get; set; }

            [Parameter("another-parameter")]
            public Int32 AnotherParameter { get; set; }


            public Object Run(Object parameter)
            {
                throw new NotImplementedException();
            }
        }

        public class ConstructorMethod
        {
            [Test(Description = "should create one-element-length array when one runnable is passed")]
            public void ShouldCreateOneElementLengthArray_WhenOneRunnableIsPassed()
            {
                // Act
                var underTest = new AutoCommandDescriptorRepository(Array(typeof (TestRunnable)));

                // Arrange
                var result = underTest.GetCommandDescriptors()?.Count();

                // Assert
                Assert.That(result, Is.EqualTo(1));
            }
        }

        public class GetCommandDescriptorsMethod
        {
            [Test(Description = "should return short type name of runnable when one runnable is passed")]
            public void ShouldReturnShortTypeNameOfRunnable_WhenOneRunnableIsPassed()
            {
                // Act
                var underTest = new AutoCommandDescriptorRepository(Array(typeof (TestRunnable)));

                // Arrange
                var result = underTest.GetCommandDescriptors().SingleOrDefault();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(nameof(TestRunnable)));
            }

            [Test(Description = "should return specified description in CommandDescriptorAttribute when one runnable is passed")]
            public void ShouldReturnSpecifiedDescriptionInRunnableDescriptorAttribute_WhenOneRunnableIsPassed()
            {
                // Act
                var underTest = new AutoCommandDescriptorRepository(Array(typeof (TestRunnable)));

                // Arrange
                var result = underTest.GetCommandDescriptors().SingleOrDefault();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Description, Is.EqualTo("This is a description"));
            }

            [Test(Description = "should return specified command name in CommandDescriptorAttribute when one runnable is passed")]
            public void ShouldReturnSpecifiedCommandNameInRunnableDescriptorAttribute_WhenOneRunnableIsPassed()
            {
                // Act
                var underTest = new AutoCommandDescriptorRepository(Array(typeof (TestRunnable)));

                // Arrange
                var result = underTest.GetCommandDescriptors().SingleOrDefault();

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.CommandNames, Is.EquivalentTo(Array("test")));
            }

            [Test(Description = "should return return specified description in CommandDescriptorAttribute when one runnable is passed")]
            public void ShouldReturnSpecifiedArgumentsInRunnableDescriptorAttribute_WhenOneRunnableIsPassed()
            {
                // Act
                var underTest = new AutoCommandDescriptorRepository(Array(typeof (TestRunnable)));

                // Arrange
                var result = underTest.GetCommandDescriptors().SingleOrDefault();

                // Assert
                Assert.That(result, Is.Not.Null);

                var myParameterDescriptor = new ArgumentDescriptor()
                {
                    ArgumentName = "my-parameter",
                    ArgumentType = typeof (String),
                    DefaultValue = null,
                    IsOptional = true,
                    Position = 0
                };

                var anotherParameterDescriptor = new ArgumentDescriptor
                {
                    ArgumentName = "another-parameter",
                    ArgumentType = typeof (Int32),
                    DefaultValue = null,
                    IsOptional = true,
                    Position = 1
                };

                var parameters = Array(myParameterDescriptor, anotherParameterDescriptor);

                Assert.That(result.Arguments, Is.EquivalentTo(parameters));
            }
        }
    }
}