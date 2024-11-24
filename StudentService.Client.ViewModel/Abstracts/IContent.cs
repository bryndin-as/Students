using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Client.ViewModel.Abstracts
{
    public interface IContent
    {
        bool IsLoaded { get; }

        event EventHandler<EventArgs>? Initialized;

        Task InitializeAsync();
    }
}
