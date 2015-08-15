using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.ValueConverters;
using NSubstitute;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables
{
    [TestFixture]
    internal class DefaultRunnableFactoryTests : TestFixtureBase
    {
        public class TestRunnable : IRunnable
        {
            /// <summary>
            /// String or sets the value.
            /// </summary>
            [Parameter("value")]
            public Object Value { get; set; }

            /// <summary>
            /// String or sets the description.
            /// </summary>
            [Parameter("description")]
            public String Description { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            [Parameter("number")]
            public Int32 Number { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="TestRunnable"/> class.
            /// </summary>
            public TestRunnable()
            {
            }

            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public Object Run(Object parameter)
            {
                return Value;
            }
        }

        public class CreateMethod
        {
            [Test(Description = "Create should instantiate runnable when default constructor is available")]
            public void ShouldInstantiateRunnable_WhenDefaultConstructorIsAvailable()
            {
                // Given
                var actualArguments = new Dictionary<String, Object>
                {
                    {"value", "test"},
                    {"description", "some description"}
                };
                var underTest = new DefaultRunnableFactory(new[] {typeof (TestRunnable)});

                // When
                var result = underTest.Create("TestRunnable", actualArguments) as TestRunnable;

                // Then
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value, Is.EqualTo("test"));
                Assert.That(result.Description, Is.EqualTo("some description"));
            }

            [Test(Description = "Create should convert double to integer value when parameter type is Int32")]
            public void ShouldConvertDoubleToIntegerValue_WhenParameterTypeIsInt32AndIntegerValueConverterIsPassed()
            {
                // Given
                var actualArguments = new Dictionary<String, Object>()
                {
                    {"number", 10.0}
                };
                var valueConverter = Substitute.For<IntegerValueConverter>();
                var underTest = new DefaultRunnableFactory(new[] {typeof (TestRunnable)}, valueConverter);

                // When
                var result = underTest.Create("TestRunnable", actualArguments) as TestRunnable;

                // Then
                Assert.That(result.Number, Is.EqualTo(10));
                valueConverter.Received(1).CanConvert(10.0, typeof (Int32));
                valueConverter.Received(1).Convert(10.0);
            }
        }
    }
}