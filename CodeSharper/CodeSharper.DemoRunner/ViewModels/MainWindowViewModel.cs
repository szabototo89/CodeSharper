using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.DemoRunner.Models;

namespace CodeSharper.DemoRunner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<DemoApplicationDescriptor> DemoApplications { get; set; }

        public MainWindowViewModel()
        {
            DemoApplications =new ObservableCollection<DemoApplicationDescriptor>();
        }
    }
}
