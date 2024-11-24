using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Client.ViewModel.Abstracts
{
    public abstract class ContentBase : Notifier, IContent
    {
        private bool isLoaded;
        public bool IsLoaded
        {
            get => isLoaded;
            set
            {
                isLoaded = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler<EventArgs>? Initialized;

        public virtual async Task InitializeAsync()
        {
            await Task.CompletedTask;
            IsLoaded = true;
            Initialized?.Invoke(this, EventArgs.Empty);
        }

        private IAsyncRelayCommand? loadCommand;
        public IAsyncRelayCommand LoadCommand =>
            loadCommand ??= new AsyncRelayCommand(InitializeAsync);


        private bool inProgress;
        public bool InProgress
        {
            get => inProgress;
            set
            {
                inProgress = value;
                OnPropertyChanged();
            }
        }
    }
}
