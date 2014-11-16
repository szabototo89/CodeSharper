using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using CodeSharper.DemoRunner.Models;
using CodeSharper.DemoRunner.ViewModels;

namespace CodeSharper.DemoRunner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var viewModel = new MainWindowViewModel() {
                DemoApplications = new ObservableCollection<DemoApplicationDescriptor>(new[] {
                    new DemoApplicationDescriptor() { Name = "Demo1", Description = "Lorem ipsum ..."}
                })
            };

            viewModel.InitializeDemoApplicationsFromAssembly(Assembly.GetExecutingAssembly());

            var window = new MainWindow() {
                DataContext = viewModel
            };

            window.Show();
        }
    }
}
