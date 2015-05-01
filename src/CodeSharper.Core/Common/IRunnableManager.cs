using System.Collections.Generic;
using System.Net.Mail;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common
{
    public interface IRunnableManager
    {
        /// <summary>
        /// Gets the consumers of specified runnable
        /// </summary>
        IEnumerable<IValueConsumer> GetConsumers(IRunnable runnable);

        /// <summary>
        /// Gets the producers of specified runnable
        /// </summary>
        IEnumerable<IValueProducer> GetProducers(IRunnable runnable);
    }
}