

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using CodeSharper.Core.Commands;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.CommandManager
{
    [TestFixture]
    internal class CommandCallDescriptorTextFixture : TestFixtureBase
    {
        [Test(Description = "CommandCallDescriptor should have command name and parameter value and parameter name")]
        public void CommandCallDescriptorShouldHaveCommandNameAndParameterValueAndParameterName()
        {
            // Given
            var underTest = new CommandCallDescriptor(
                name: "command-name",
                arguments: new Object[] { "hello world!" },
                namedArguments: new Dictionary<String, Object> { { "param1", 12 } }
                );

            // When
            var result = new {
                Name = underTest.Name,
                Parameters = underTest.Arguments,
                NamedParameters = underTest.NamedArguments
            };


            // Then
            Assert.That(result.Name, Is.EqualTo("command-name"));
            Assert.That(result.Parameters, Is.EquivalentTo(new[] { "hello world!" }));

            var param = result.NamedParameters.FirstOrDefault();
            Assert.That(param, Is.Not.Null);
            Assert.That(param.Key, Is.EqualTo("param1"));
            Assert.That(param.Value, Is.EqualTo(12));
        }
    }
}