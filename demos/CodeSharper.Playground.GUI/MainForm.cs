using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.ValueConverters;
using CodeSharper.Core.Services;
using CodeSharper.Core.Texts;
using CodeSharper.Interpreter.Bootstrappers;
using CodeSharper.Playground.GUI.Modules;
using CodeSharper.Playground.GUI.Services;
using Microsoft.CodeAnalysis;
using static CodeSharper.Core.Utilities.ConstructsHelper;

namespace CodeSharper.Playground.GUI
{
    public partial class MainForm : Form
    {
        private CompilerModuleBase compilerModule;
        private readonly Stopwatch stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            stopwatch = new Stopwatch();

            var assemblies = new[] {Assembly.Load("CodeSharper.Core"), Assembly.Load("CodeSharper.Languages"), Assembly.Load("CodeSharper.Languages.CSharp"), Assembly.GetExecutingAssembly()};
            var runnableTypeResolver = new AutoRunnableResolver();
            var valueConverter = new IntegerValueConverter();
            var interactiveService = new ReplaceTextInteractiveService(this);
            var runnableFactory = new DefaultRunnableFactory(runnableTypeResolver.ResolveRunnableTypes(assemblies), valueConverter, interactiveService: interactiveService);
            var fileDescriptorRepository = new FileDescriptorRepository("descriptors.json", assemblies);
            var autoDescriptorRepository = new AutoCommandDescriptorRepository(assemblies.SelectMany(assembly => assembly.GetTypes()));
            var descriptorRepository = new MultiDescriptorRepository(
                Array<IDescriptorRepository>(fileDescriptorRepository, autoDescriptorRepository)
            );

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
                return;

            var stream = fileDialog.OpenFile();
            var streamReader = new StreamReader(stream);
            sourceEditor.Text = streamReader.ReadToEnd();

            Text = fileDialog.FileName;
        }

        private void saveAsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

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
                return;
            var tag = menuItem.Tag as String;
        }

        private async void refactorButton_Click(Object sender, EventArgs e)
        {
            runTimeValueLabel.Text += " (running ...)";

            var result = await executeQuery(queryEditor.Text, sourceEditor.Text);

            if (result.HasValue)
            {
                sourceEditor.Text = result.Value.Source;
                resultEditor.Text = result.Value.Results;
            }

            runTimeValueLabel.Text = stopwatch.Elapsed.ToString();
        }

        private async Task<DocumentResults?> executeQuery(String query, String source)
        {
            stopwatch.Restart();
            stopwatch.Start();
            var result = await Task.Run(() => CompilerModule.ExecuteQuery(query, source));
            stopwatch.Stop();
            return result;
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

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compilerModule = new CSharpCompilerModule(Bootstrapper);
        }
    }
}