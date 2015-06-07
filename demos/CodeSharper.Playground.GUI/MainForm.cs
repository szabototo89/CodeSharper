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
using CodeSharper.Core.Common.Runnables.ValueConverters;
using CodeSharper.Core.Services;
using CodeSharper.Core.Texts;
using CodeSharper.Interpreter.Bootstrappers;
using CodeSharper.Playground.GUI.Modules;

namespace CodeSharper.Playground.GUI
{
    public partial class MainForm : Form
    {
        private CompilerModuleBase compilerModule;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            var assemblies = new[] {Assembly.Load("CodeSharper.Core"), Assembly.Load("CodeSharper.Languages"), Assembly.GetExecutingAssembly()};
            var runnableTypeResolver = new AutoRunnableResolver();
            var valueConverter = new IntegerValueConverter();
            var runnableFactory = new DefaultRunnableFactory(runnableTypeResolver.ResolveRunnableTypes(assemblies), valueConverter);
            var descriptorRepository = new FileDescriptorRepository("descriptors.json", assemblies);
            Bootstrapper = new Bootstrapper(runnableFactory, descriptorRepository);
            compilerModule = new TextCompilerModule(Bootstrapper);
        }

        public CompilerModuleBase CompilerModule
        {
            get { return compilerModule; }
        }

        public Bootstrapper Bootstrapper { set; get; }

        private void openToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var stream = fileDialog.OpenFile();
            var streamReader = new StreamReader(stream);
            sourceEditor.Text = streamReader.ReadToEnd();

            Text = fileDialog.FileName;
        }

        private void saveAsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var file = File.CreateText(fileDialog.FileName))
            {
                file.WriteLine(sourceEditor.Text);
            }
        }

        private void newToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            sourceEditor.Text = String.Empty;
        }

        private void languageSelectionChanged(Object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
            {
                return;
            }
            var tag = menuItem.Tag as String;
        }

        private void refactorButton_Click(Object sender, EventArgs e)
        {
            var result = CompilerModule.ExecuteQuery(queryEditor.Text, sourceEditor.Text);
            sourceEditor.Text = result.Source;
            resultEditor.Text = result.Results;
        }

        private void csvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compilerModule = new CsvCompilerModule(Bootstrapper);
        }

        private void jsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compilerModule = new JsonCompilerModule(Bootstrapper);
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compilerModule = new TextCompilerModule(Bootstrapper);
        }
    }
}