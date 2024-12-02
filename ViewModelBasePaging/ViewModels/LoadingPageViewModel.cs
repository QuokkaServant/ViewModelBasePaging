using System.Windows.Input;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

namespace ViewModelBasePaging.ViewModels
{
    public class LoadingPageViewModel : PageViewModelBase
    {
        public enum Process
        {
            Loading,

            Complated
        }

        public Process? CurrentProcess
        {
            get => currentProcess;
            private set => SetProperty(ref currentProcess, value);
        }
        private Process? currentProcess;

        public ICommand NextPageCommand { init; get; }

        public ICommand PreviousPageCommand { init; get; }

        public LoadingPageViewModel()
        {
            NextPageCommand = new RelayCommand(OnNextPage);
            PreviousPageCommand = new RelayCommand(OnPreviousPage);
        }

        protected override void OnLoaded(object? parameter)
        {
            if (parameter is not int count)
                CurrentProcess = Process.Complated;
            else
                LoadingProcess(count);

            async void LoadingProcess(int delay)
            {
                CurrentProcess = Process.Loading;
                await Task.Run(() => { Task.Delay(delay * 1000).Wait(); });
                CurrentProcess = Process.Complated;
            }

            base.OnLoaded(parameter);
        }

        protected override void OnUnloaded()
        {
            CurrentProcess = null;
            base.OnUnloaded();
        }

        #region [Method-] OnNextPage
        private void OnNextPage()
        {
            NavigatePage(ViewModelLocator.Instance.EndPageViewModel);
        }
        #endregion

        #region [Method-] OnPreviousPage
        private void OnPreviousPage()
        {
            NavigatePage(ViewModelLocator.Instance.ActionPageViewModel);
        }
        #endregion
    }
}
