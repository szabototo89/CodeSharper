using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common
{
    public class DefaultRunnableManager : IRunnableManager
    {
        /// <summary>
        /// Gets the consumers of specified runnable
        /// </summary>
        public IEnumerable<IValueConsumer> GetConsumers(IRunnable runnable)
        {
            Assume.NotNull(runnable, nameof(runnable));
            var type = runnable.GetType();

            var attributes = type.GetCustomAttributes(inherit: true)
                                 .OfType<ConsumesAttribute>();

            var consumers = attributes.Select(attribute => attribute.ConsumerType)
                                      .Select(consumeType => (IValueConsumer) Activator.CreateInstance(consumeType));

            return consumers;
        }

        /// <summary>
        /// Gets the producers of specified runnable
        /// </summary>
        public IEnumerable<IValueProducer> GetProducers(IRunnable runnable)
        {
            Assume.NotNull(runnable, nameof(runnable));
            var type = runnable.GetType();

            var attributes = type.GetCustomAttributes(inherit: true)
                                 .OfType<ProducesAttribute>();

            var producers = attributes.Select(attribute => attribute.ProducerType)
                                      .Select(producerType => (IValueProducer) Activator.CreateInstance(producerType));

            return producers;
        }
    }
}