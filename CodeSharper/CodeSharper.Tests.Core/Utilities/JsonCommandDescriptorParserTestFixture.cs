﻿using System;
using System.Linq;
using System.Security.AccessControl;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class JsonCommandDescriptorParserTestFixture
    {
        [SetUp]
        public void Setup() { }

        [TearDown]
        public void Teardown() { }

        [Test]
        public void JsonCommandDescriptorParserShouldBeAbleToParseJsonFile()
        {
            // Given
            var json =
                "{\r\n    \"name\": \"Find Text Command\",\r\n    \"command-names\": [ \"find-text\" ],\r\n    \"arguments\": [\r\n        {\r\n            \"name\": \"pattern\",\r\n            \"type\": \"System.String\",\r\n            \"optional\": false,\r\n            \"default-value\": null\r\n        }\r\n    ]\r\n}";

            // When
            var result = JsonCommandDescriptorParser.ParseFrom(json);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Find Text Command"));
            Assert.That(result.CommandNames, Is.EquivalentTo(new[] { "find-text" }));

            var argument = result.Arguments.OfType<NamedArgumentDescriptor>().Single();
            Assert.That(argument.ArgumentName, Is.EqualTo("pattern"));
            Assert.That(argument.ArgumentType, Is.EqualTo(typeof(String)));
            Assert.That(argument.IsOptional, Is.False);
            Assert.That(argument.DefaultValue, Is.Null);
        }
    }
}