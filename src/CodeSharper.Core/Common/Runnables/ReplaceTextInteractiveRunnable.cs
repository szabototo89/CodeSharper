using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Services;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof (ContainsTextRangeMultiValueConsumer))]
    public class ReplaceTextInteractiveRunnable :
        RunnableBase<IEnumerable<TextRange>, IEnumerable<TextRange>, GreadyEnumerableCastingHelper<TextRange>>
    {
        private readonly IServiceFactory serviceFactory;
        private readonly IInteractiveService interactiveService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextInteractiveRunnable"/> class.
        /// </summary>
        public ReplaceTextInteractiveRunnable(IServiceFactory serviceFactory)
        {
            Assume.IsRequired(serviceFactory, nameof(serviceFactory));

            this.serviceFactory = serviceFactory;
            this.interactiveService = serviceFactory.GetService<IInteractiveService>();
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override IEnumerable<TextRange> Run(IEnumerable<TextRange> parameters)
        {
            /*var textRanges = parameters.ToArray();
            foreach (var parameter in textRanges)
            {
                Console.WriteLine("New value ({0})", parameter.GetText());
                var value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                    parameter.ChangeText(value);
            }

            return textRanges;*/

            return interactiveService.Transform(parameters).OfType<TextRange>();
        }
    }
}