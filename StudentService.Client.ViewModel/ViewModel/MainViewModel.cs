using StudentService.Client.Core;
using StudentService.Client.ViewModel.Abstracts;
using StudentService.Client.ViewModel.Infrastructure;

namespace StudentService.Client.ViewModel.ViewModel
{
    public class MainViewModel : ContentBase
    {
        public IDataService _dataService;

        public NavigationService Navigation => NavigationService.Default();

        private readonly Lazy<CmmonViewModel> _cmmonViewModel;
        private readonly Lazy<StudentViewModel> _studentViewModel;
        private readonly Lazy<SubjectViewModel> _subjectViewModel;

        public CmmonViewModel CmmonViewModel => _cmmonViewModel.Value;
        public StudentViewModel StudentViewModel => _studentViewModel.Value;
        public SubjectViewModel SubjectViewModel => _subjectViewModel.Value;

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged();
                UpdateCurrentContent();
            }
        }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _cmmonViewModel = new Lazy<CmmonViewModel>(() => new CmmonViewModel(_dataService));
            _studentViewModel = new Lazy<StudentViewModel>(() => new StudentViewModel(_dataService));
            _subjectViewModel = new Lazy<SubjectViewModel>(() => new SubjectViewModel(_dataService));
            Navigation.NavigateTo(new CmmonViewModel(_dataService));
        }

        private void UpdateCurrentContent()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    Navigation.NavigateTo(CmmonViewModel);
                    break;
                case 1:
                    Navigation.NavigateTo(StudentViewModel);
                    break;
                case 2:
                    Navigation.NavigateTo(SubjectViewModel);
                    break;
            }
        }
    }
}