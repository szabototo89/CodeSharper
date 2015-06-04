using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;
using NSubstitute;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class FlattenValueProducerTests : TestFixtureBase
    {
        private IRunnable<IEnumerable<TValue>, IEnumerable<IEnumerable<TValue>>> CreateRepeaterRunnable<TValue>(Int32 count)
        {
            var runnable = Substitute.For<IRunnable<IEnumerable<TValue>, IEnumerable<IEnumerable<TValue>>>>();
            runnable.Run(Arg.Any<IEnumerable<TValue>>())
                    .Returns(context => Enumerable.Repeat(context.Arg<IEnumerable<TValue>>(), count));
            return runnable;
        }

        [TestCase(1)]
        [TestCase(true)]
        [TestCase("Hello World!")]
        [Test(Description = "FlattenValueProducer should flatten multi value arguments when multiple values are passed")]
        public void FlattenValueProducer_ShouldFlattenMultiValueArguments_WhenMultipleValuesArePassed(Object value)
        {
            // Given
            var underTest = new FlattenValueProducer<Object>();
            var runnable = CreateRepeaterRunnable<Object>(3);

            TypeDescriptor.AddAttributes(runnable, new ProducesAttribute(underTest.GetType()));

            // When
            var parameter = new[] {value};
            var runnableResult = runnable.Run(parameter);
            var result = new
            {
                IsConvertable = underTest.IsConvertable(runnableResult),
                Value = underTest.Convert((Object) runnableResult)
            };

            // Then
            Assert.That(result.IsConvertable, Is.True);
            Assert.That(result.Value, Is.InstanceOf<IEnumerable<Object>>());
            Assert.That(result.Value, Is.EquivalentTo(new[] {value, value, value}));
        }
    }
}