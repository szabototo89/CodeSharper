using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
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
            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
                {
                    DemoApplications = new ObservableCollection<DemoApplicationDescriptor>(new[]
                    {
                        new DemoApplicationDescriptor() { Name = "Demo1", Description = "Lorem ipsum ..."}
                    })
                }
            };

            window.Show();
        }
    }
}
