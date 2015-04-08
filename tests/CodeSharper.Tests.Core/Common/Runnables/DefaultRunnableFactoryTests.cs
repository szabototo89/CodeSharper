﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
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
            public Object Value { get; set; }

            /// <summary>
            /// String or sets the description.
            /// </summary>
            public String Description { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="TestRunnable"/> class.
            /// </summary>
            public TestRunnable(Object value, String description)
            {
                Value = value;
                Description = description;
            }

            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public Object Run(Object parameter)
            {
                return Value;
            }
        }

        [Test(Description = "Create should instantiate runnable when non-default constructor is available")]
        public void Create_ShouldInstantiateRunnable_WhenNonDefaultConstructorIsAvailable()
        {
            // Given
            var actualArguments = new Dictionary<String, Object> { { "test", "some description" } };
            var underTest = new DefaultRunnableFactory(new[] { typeof(TestRunnable) });

            // When
            var result = underTest.Create("TestRunnable", actualArguments) as TestRunnable;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("test"));
            Assert.That(result.Description, Is.EqualTo("some description"));
        }
    }
}
