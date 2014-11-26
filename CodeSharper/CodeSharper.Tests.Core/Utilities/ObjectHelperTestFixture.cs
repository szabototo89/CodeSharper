﻿using System;
using System.Collections.Generic;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.Mocks;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class ObjectHelperTestFixture
    {
        [Test]
        public void EveryObjectShouldAbleToConvertToArray()
        {
            // Given
            var underTest = 5;

            // When
            var result = underTest.WrapToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { 5 }));
        }

        [Test]
        public void WrapToArrayShouldAbleToHandleStrings()
        {
            // Given
            var underTest = "hello world";

            // When
            var result = underTest.WrapToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { "hello world" }));
        }

        [Test]
        public void WrapToArrayShouldAbleToHandleEnumerable()
        {
            // Given
            var underTest = new[] { "hello world" };

            // When
            var result = underTest.WrapToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { new[] { "hello world" } }));
        }

        [Test]
        public void WrapToListShouldAbleToConvertEveryObjectToList()
        {
            // Given
            var underTest = 5;

            // When
            var result = underTest.WrapToList();

            // Then
            Assert.That(result, Is.InstanceOf<List<Int32>>());
            Assert.That(result, Is.EquivalentTo(new[] { 5 }));
        }

        [Test]
        public void SafeShouldBeAbleToInvokeSafelyEveryTypeOfObject()
        {
            // Given
            GeneralObjectMocks.Person underTest = null;

            // When
            var result = underTest.Safe();

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.Null);
            Assert.That(result.Age, Is.EqualTo(0));
        }

        [Test]
        public void SafeShouldReturnItsValueIfItIsNotNull()
        {
            // Given
            var underTest = new GeneralObjectMocks.Person() { Name = "John Doe", Age = 34 };

            // When
            var result = underTest.Safe();

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Age, Is.EqualTo(34));
        }

        [Test]
        public void SafeOrDefaultShouldReturnDefaultValueIfObjectIsNull()
        {
            // Given
            GeneralObjectMocks.Person underTest = null;

            // When
            var result = underTest.SafeOrDefault(GeneralObjectMocks.Persons.JohnDoe);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(GeneralObjectMocks.Persons.JohnDoe.Name));
            Assert.That(result.Age, Is.EqualTo(GeneralObjectMocks.Persons.JohnDoe.Age));
        }

        [Test]
        public void WithShouldAbleToChangeValueState()
        {
            // Given
            var underTest = new GeneralObjectMocks.Person();

            // When
            var result = underTest.With(person => {
                person.Name = "Jane Doe";
                person.Age = 24;
            });

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(underTest));
            Assert.That(result.Name, Is.EqualTo("Jane Doe"));
            Assert.That(result.Age, Is.EqualTo(24));
        }
    }
}
