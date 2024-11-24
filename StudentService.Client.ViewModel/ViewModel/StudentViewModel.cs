using CommunityToolkit.Mvvm.Input;
using StudentService.Client.Core;
using StudentService.Client.ViewModel.Abstracts;
using StudentService.DAL.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentService.Client.ViewModel.ViewModel
{
    public class StudentViewModel : ContentBase
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

        private string _studentName;
        public string StudentName
        {
            get => _studentName;
            set
            {
                _studentName = value;
                OnPropertyChanged();
            }
        }

        private string _studentSurname;
        public string StudentSurname
        {
            get => _studentSurname;
            set
            {
                _studentSurname = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentBaseDTO>? _students;
        public ObservableCollection<StudentBaseDTO> Students
        {
            get => _students ??= new();
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        public StudentViewModel(IDataService dataService)
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

        private IAsyncRelayCommand? _getStudentsCommand;
        public IAsyncRelayCommand GetStudentsCommand
        {
            get => _getStudentsCommand ??= new AsyncRelayCommand(GetStudentsAsync);
        }

        private async Task GetStudentsAsync()
        {
            try
            {
                var students = await _dataService.GetStudentsAsync().ConfigureAwait(false);
                Students = new ObservableCollection<StudentBaseDTO>(students);
            }
            catch (Exception ex)
            {
                // Логирование
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private IAsyncRelayCommand? _createStudentsCommand;
        public IAsyncRelayCommand CreateStudentsCommand
        {
            get => _createStudentsCommand ??= new AsyncRelayCommand(CreateStudentsAsync);
        }

        private async Task CreateStudentsAsync()
        {
            try
            {
                var studentCreateDTO = new StudentCreateDTO()
                {
                    Name = StudentName,
                    Surname = StudentSurname
                };

                var studentId = await _dataService.CreateStudentAsync(studentCreateDTO);

                if (studentId > 0)
                {
                    Students.Add(new StudentBaseDTO
                    {
                        Id = studentId,
                        Name = StudentName,
                        Surname = StudentSurname
                    });
                    IsPopupOpen = false;
                    StudentName = string.Empty;
                    StudentSurname = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Логирование
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }
    }
}