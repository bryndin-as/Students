using CommunityToolkit.Mvvm.Input;
using StudentService.Client.Core;
using StudentService.Client.ViewModel.Abstracts;
using StudentService.DAL.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentService.Client.ViewModel.ViewModel
{
    public class SubjectViewModel : ContentBase
    {
        private readonly IDataService _dataService;

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                _isPopupOpen = value;
                OnPropertyChanged();
            }
        }

        private string _subjectName;
        public string SubjectName
        {
            get => _subjectName;
            set
            {
                _subjectName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SubjectBaseDTO>? _subjects;
        public ObservableCollection<SubjectBaseDTO> Subjects
        {
            get => _subjects ??= new();
            set
            {
                _subjects = value;
                OnPropertyChanged();
            }
        }

        public SubjectViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        private ICommand? _openPopupCommand;
        public ICommand OpenPopupCommand
        {
            get => _openPopupCommand ??= new RelayCommand(() =>
            {
                IsPopupOpen = true;
            });
        }

        private IAsyncRelayCommand? _getSubjectCommand;
        public IAsyncRelayCommand GetSubjectCommand
        {
            get => _getSubjectCommand ??= new AsyncRelayCommand(GetSubjectAsync);
        }

        private async Task GetSubjectAsync()
        {
            try
            {
                var subjects = await _dataService.GetSubjectsAsync();
                Subjects = new ObservableCollection<SubjectBaseDTO>(subjects);
                
            }
            catch (Exception ex)
            {
                // Логирование
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private IAsyncRelayCommand? _createSubjectCommand;
        public IAsyncRelayCommand CreateSubjectCommand
        {
            get => _createSubjectCommand ??= new AsyncRelayCommand(CreateSubjectAsync);
        }

        private async Task CreateSubjectAsync()
        {
            var subjectCreateDTO = new SubjectCreateDTO()
            {
                Name = SubjectName,
            };

            var subjectId = await _dataService.CreateSubjectAsync(subjectCreateDTO);

            if (subjectId > 0)
            {
                Subjects.Add(new SubjectBaseDTO
                {
                    Id = subjectId,
                    Name = SubjectName,
                });
                IsPopupOpen = false;
                SubjectName = string.Empty;
            }
        }
    }
}