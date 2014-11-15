using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.DemoRunner.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<DemoApplication> DemoApplications { get; private set; }

        public MainWindowViewModel()
        {
            DemoApplications =new ObservableCollection<DemoApplication>();
        }
    }
}
