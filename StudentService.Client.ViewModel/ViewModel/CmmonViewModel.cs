using CommunityToolkit.Mvvm.Input;
using StudentService.Client.Core;
using StudentService.Client.ViewModel.Abstracts;
using StudentService.DAL.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StudentService.Client.ViewModel.ViewModel
{
    public class CmmonViewModel : ContentBase
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

        private string _countEntities;
        public string CountEntities
        {
            get => _countEntities;
            set
            {
                _countEntities = value;
                OnPropertyChanged();
            }
        }

        public CmmonViewModel(IDataService dataService)
        {
            _dataService = dataService;
            InitializeDataAsync().ConfigureAwait(false);
        }


        private async Task InitializeDataAsync()
        {
            try
            {
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации: {ex.Message}");
            }
        }

        private ICommand? _openPopupCommand;
        public ICommand OpenPopupCommand
        {
            get => _openPopupCommand ??= new RelayCommand(() =>
            {
                IsPopupOpen = true;
            });
        }

        private ObservableCollection<StudentWithGradesDTO> studentWithGrades; 
        public ObservableCollection<StudentWithGradesDTO> StudentWithGrades
        {
            get => studentWithGrades ??= new();
            set
            {
                studentWithGrades = value;
                OnPropertyChanged();
            }
        }

        private IAsyncRelayCommand? getStudentWithGradesCommand;
        public IAsyncRelayCommand GetStudentWithGradesCommand
        {
            get => getStudentWithGradesCommand ??= new AsyncRelayCommand(GetStudentWithGradesAsync);
        }

        private async Task GetStudentWithGradesAsync()
        {
            try
            {
                var studentsWithGrades = await _dataService.GetStudentWithGradesAsync();
                StudentWithGrades = new ObservableCollection<StudentWithGradesDTO>(studentsWithGrades);

            }
            catch (Exception ex)
            {
                // Логирование
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private IAsyncRelayCommand? _generadeSeedData;
        public IAsyncRelayCommand GeneradeSeedData
        {
            get => _generadeSeedData ??= new AsyncRelayCommand(GeneradeSeedDataAsync);
        }

        private async Task GeneradeSeedDataAsync()
        {
            try
            {
                if (!TryParseCount(CountEntities, out int count))
                {
                    Console.WriteLine("Не удалось преобразовать сообщение в число");
                    return;
                }
                await _dataService.AddSeedTest(count);
                await RefreshDataAsync();
                ResetState();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка генерации данных: {ex.Message}");
                throw;
            }
        }

        private bool TryParseCount(string countEntities, out int count)
        {
            if (int.TryParse(countEntities, out count))
                return true;

            Console.WriteLine("Некорректное значение для числа");
            return false;
        }

        private async Task RefreshDataAsync()
        {
            try
            {
                var studentsWithGrades = await _dataService.GetStudentWithGradesAsync();
                StudentWithGrades = new ObservableCollection<StudentWithGradesDTO>(studentsWithGrades);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при обновлении данных {ex.Message}");
            }
        }

        private void ResetState()
        {
            IsPopupOpen = false;
            CountEntities = string.Empty;
        }
    }
}