using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeSharper.Core.Services;

namespace CodeSharper.Playground.GUI.Services
{
    public class ReplaceTextInteractiveService : IInteractiveService
    {
        private readonly Form parentForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextInteractiveService"/> class.
        /// </summary>
        public ReplaceTextInteractiveService(Form parentForm)
        {
            this.parentForm = parentForm;
        }

        /// <summary>
        /// Transforms the specified parameters.
        /// </summary>
        public IEnumerable<Object> Transform(IEnumerable<Object> parameters)
        {
            var dialog = new ReplaceTextDialog(parameters)
            {
                StartPosition = FormStartPosition.CenterParent
            };

            dialog.ShowDialog();

            return parameters;
        }
    }
}