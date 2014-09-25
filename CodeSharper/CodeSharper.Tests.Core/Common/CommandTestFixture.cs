using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Values;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    class CommandTestFixture
    {
        [Test]
        public void IdentityCommandShouldReturnWithPassedValue()
        {
            // Given
            var parameter = Arguments.Value("Hello World!");
            var underTest = new IdentityCommand();

            // When
            var result = underTest.Execute(parameter);

            // Then
            Assert.That(result, Is.EqualTo(parameter));
        }

        [Test]
        public void StringCommandShouldReturnStringOfValue()
        {
            // Given
            var underTest = new ToStringCommand();

            // When
            var result = underTest.Execute(Arguments.Value(45 + 5));

            // Then
            Assert.That(result, Is.EqualTo(Arguments.Value("50")));
        }


        [Test]
        public void WriteToConsoleCommandShouldPrintValueToConsoleOutput()
        {
            // Given
            var underTest = new WriteToConsoleCommand();

            // When
            var value = Arguments.Value("Hello World!");
            var result = underTest.Execute(value);

            // Then
            Assert.That(value, Is.EqualTo(result));                                                            
        }


        //[Test]
        //public void MultiCommandShouldExecuteMultipleInputs()
        //{
        //    // Given
        //    var underTest = new IdentityCommand();

        //    // When
        //    var range = Enumerable.Range(1, 10);
        //    var result = underTest.ExecuteWith<MultiCommandExecutor>(Arguments.MultiValue(range)) as MultiValueArgument;

        //    // Then
        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result.Source, Is.EqualTo(range.Select(Arguments.Value)));
        //}

    }
}
