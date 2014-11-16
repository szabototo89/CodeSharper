using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.DemoRunner.Common;
using CodeSharper.DemoRunner.DemoApplications;
using CodeSharper.DemoRunner.Models;

namespace CodeSharper.DemoRunner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ActionCommand _runDemoCommand;
        private DemoApplicationDescriptor _selectedDemoApplicationDescriptor;
        public ObservableCollection<DemoApplicationDescriptor> DemoApplications { get; set; }

        public MainWindowViewModel()
        {
            DemoApplications = new ObservableCollection<DemoApplicationDescriptor>();
            initalizeCommands();
        }

        private void initalizeCommands()
        {
            _runDemoCommand = new ActionCommand(_ => {
                if (SelectedDemoApplicationDescriptor.Run != null)
                    SelectedDemoApplicationDescriptor.Run();
            });
        }

        public DemoApplicationDescriptor SelectedDemoApplicationDescriptor
        {
            get { return _selectedDemoApplicationDescriptor; }
            set
            {
                _selectedDemoApplicationDescriptor = value;
                OnPropertyChanged();
            }
        }

        public void InitializeDemoApplicationsFromAssembly(Assembly assembly)
        {
            Constraints.NotNull(() => assembly);

            DemoApplications.Clear();

            var types = assembly.GetTypes()
                                .Where(type => type.GetInterfaces().Contains(typeof(IDemoApplication)))
                                .ToArray();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<DemoAttribute>(true);
                var descriptor = new DemoApplicationDescriptor() {
                    Name = attribute.Name,
                    Description = attribute.Description,
                };
                Type instanceType = type;  // because of different behaviour of C# compilers
                descriptor.Run = delegate {
                    var instance = Activator.CreateInstance(instanceType) as IDemoApplication;
                    instance.Run(null);
                };

                DemoApplications.Add(descriptor);
            }
        }

        public ActionCommand RunDemoCommand
        {
            get { return _runDemoCommand; }
        }
    }
}
