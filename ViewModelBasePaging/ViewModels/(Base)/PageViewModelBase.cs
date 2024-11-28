using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

namespace ViewModelBasePaging.ViewModels
{
    public abstract class PageViewModelBase : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        protected static MainWindowViewModel MainWindowViewModel => ViewModelLocator.Instance.MainWindowViewModel;

        public ICommand LoadedCommand { private init; get; }

        public ICommand UnloadedCommand { private init; get; }

        public ICommand NavigatePageCommand { private init; get; }

        public ICommand HomePageCommand { private init; get; }

        protected PageViewModelBase()
        {
            LoadedCommand = new RelayCommand<object?>(OnLoaded);
            UnloadedCommand = new RelayCommand(OnUnloaded);
            NavigatePageCommand = new RelayCommand<PageViewModelBase>(NavigatePage);
            HomePageCommand = new RelayCommand(NavigateHomePage);
        }

        /// <summary>
        /// <see cref="FrameworkElement.Loaded"/> command.
        /// </summary>
        /// <param name="parameter">Add it if necessary.</param>
        protected virtual void OnLoaded(object? parameter)
        {
            Debug.WriteLine($"Loaded {this} page. {parameter}");
        }

        /// <summary>
        /// <see cref="FrameworkElement.Unloaded"/> command.
        /// </summary>
        protected virtual void OnUnloaded()
        {
            Debug.WriteLine($"Unloaded {this} page.");
        }

        /// <summary>
        /// Go to the page that corresponds to <paramref name="pageViewModel"/>.
        /// </summary>
        /// <param name="pageViewModel"><see cref="PageViewModelBase"/> that you want to move around. If <see langword="null"/>, go to the <see cref="HomePageViewModel"/>.</param>
        protected virtual void NavigatePage(PageViewModelBase? pageViewModel)
        {
            MainWindowViewModel.CurrentPageSource = pageViewModel ?? ViewModelLocator.Instance.HomePageViewModel;
        }

        /// <summary>
        /// Return to the home page.
        /// </summary>
        protected void NavigateHomePage()
        {
            NavigatePage(ViewModelLocator.Instance.HomePageViewModel);
        }
    }
}