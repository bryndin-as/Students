using StudentService.Client.Core;
using StudentService.Client.ViewModel.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentService.Client.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataService _dataService;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            _dataService = new DataService("http://localhost:8099/");
            var viewModel = new MainViewModel(_dataService);
            DataContext = viewModel;
            await viewModel.InitializeAsync();
        }
    }
}