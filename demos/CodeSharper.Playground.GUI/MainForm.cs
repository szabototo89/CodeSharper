using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Services;
using CodeSharper.Core.Texts;
using CodeSharper.Interpreter.Bootstrappers;

namespace CodeSharper.Playground.GUI
{
    public partial class MainForm : Form
    {
        private Bootstrapper _bootstrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            var assemblies = new[] { Assembly.Load("CodeSharper.Core"), Assembly.Load("CodeSharper.Languages"), Assembly.GetExecutingAssembly() };
            var runnableTypeResolver = new AutoRunnableResolver();
            var runnableFactory = new DefaultRunnableFactory(runnableTypeResolver.ResolveRunnableTypes(assemblies));
            var descriptorRepository = new FileDescriptorRepository("descriptors.json", assemblies);
            _bootstrapper = new Bootstrapper(runnableFactory, descriptorRepository);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            var stream = fileDialog.OpenFile();
            var streamReader = new StreamReader(stream);
            sourceEditor.Text = streamReader.ReadToEnd();

            Text = fileDialog.FileName;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var file = File.CreateText(fileDialog.FileName))
            {
                file.WriteLine(sourceEditor.Text);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sourceEditor.Text = String.Empty;
        }

        private void languageSelectionChanged(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            var tag = menuItem.Tag as String;
        }

        private void refactorButton_Click(object sender, EventArgs e)
        {
            try
            {
                var textDocument = new TextDocument(sourceEditor.Text);
                // root = CsvCompiler.Parse(content);
                Console.Write("> ");
                var response = queryEditor.Text + " | @convert-to-string";
                var controlFlowDescriptor = _bootstrapper.Compiler.Parse(response);
                var controlFlow = _bootstrapper.ControlFlowFactory.Create(controlFlowDescriptor);
                var result = controlFlow.Execute(new[] { textDocument.TextRange }) as String;
                // content = root.TextRange.GetText();
                sourceEditor.Text = textDocument.Text;
                resultEditor.Text = result;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error message: {0}", exception.Message);
                Console.WriteLine("Stacktrace: ");
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}
