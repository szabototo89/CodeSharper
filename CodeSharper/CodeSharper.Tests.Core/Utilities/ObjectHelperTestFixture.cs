using System;
using System.Collections.Generic;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.Mocks;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class ObjectHelperTestFixture : TestFixtureBase
    {
        [Test(Description = "Every object should able to convert to array")]
        public void EveryObjectShouldAbleToConvertToArray()
        {
            // Given
            var underTest = 5;

            // When
            var result = underTest.WrapToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { 5 }));
        }

        [Test(Description = "WrapToArray should able to handle strings")]
        public void WrapToArrayShouldAbleToHandleStrings()
        {
            // Given
            var underTest = "hello world";

            // When
            var result = underTest.WrapToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { "hello world" }));
        }

        [Test(Description = "WrapToArray should able to handle enumerable")]
        public void WrapToArrayShouldAbleToHandleEnumerable()
        {
            // Given
            var underTest = new[] { "hello world" };

            // When
            var result = underTest.WrapToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { new[] { "hello world" } }));
        }

        [Test(Description = "WrapToList should able to convert every object to list")]
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

        [Test(Description = "Safe should be able to invoke safely every type of object")]
        public void SafeShouldBeAbleToInvokeSafelyEveryTypeOfObject()
        {
            // Given
            TypeMocks.Person underTest = null;

            // When
            var result = underTest.Safe();

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.Null);
            Assert.That(result.Age, Is.EqualTo(0));
        }

        [Test(Description = "Safe should return its value if it is not null")]
        public void SafeShouldReturnItsValueIfItIsNotNull()
        {
            // Given
            var underTest = new TypeMocks.Person() { Name = "John Doe", Age = 34 };

            // When
            var result = underTest.Safe();

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Age, Is.EqualTo(34));
        }

        [Test(Description = "SafeOrDefault should return default value if object is null")]
        public void SafeOrDefaultShouldReturnDefaultValueIfObjectIsNull()
        {
            // Given
            TypeMocks.Person underTest = null;

            // When
            var result = underTest.SafeOrDefault(TypeMocks.Persons.JohnDoe);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(TypeMocks.Persons.JohnDoe.Name));
            Assert.That(result.Age, Is.EqualTo(TypeMocks.Persons.JohnDoe.Age));
        }

        [Test(Description = "With should able to change value state")]
        public void WithShouldAbleToChangeValueState()
        {
            // Given
            var underTest = new TypeMocks.Person();

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
