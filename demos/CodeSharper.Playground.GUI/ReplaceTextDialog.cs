using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Playground.GUI
{
    public partial class ReplaceTextDialog : Form
    {
        private readonly IEnumerable<TextRange> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextDialog"/> class.
        /// </summary>
        public ReplaceTextDialog(IEnumerable<Object> parameters)
        {
            InitializeComponent();
            this.parameters = parameters.OfType<TextRange>();

            foreach (var parameter in this.parameters)
            {
                var text = parameter.GetText();

                dataGridView.Rows.Add(text, text, parameter);
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            updateTextRanges();
            Close();
        }

        private void updateTextRanges()
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var textRange = row.Cells[2].Value as TextRange;
                var value = row.Cells[1].Value as String;
                var oldValue = row.Cells[0].Value as String;

                if (oldValue != value)
                {
                    textRange.ChangeText(value);
                }
            }
        }
    }
}