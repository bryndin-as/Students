using StudentService.Client.ViewModel.Abstracts;

namespace StudentService.Client.ViewModel.Infrastructure
{
    public class NavigationService : Notifier
    {
        private static readonly Lazy<NavigationService> lazy =
            new(() => new NavigationService());

        public static NavigationService Default() =>
            lazy.Value;

        private IContent? currentContent;
        public IContent? CurrentContent
        {
            get => currentContent;
            set
            {
                currentContent = value;
                OnPropertyChanged();
            }
        }

        public void NavigateTo(IContent content)
        {
            CurrentContent = content;
        }
    }
}