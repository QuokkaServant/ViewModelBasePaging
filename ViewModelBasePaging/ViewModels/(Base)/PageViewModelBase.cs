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

        public ICommand NavigatePageCommand { private init; get; }

        public ICommand HomePageCommand { private init; get; }

        protected PageViewModelBase()
        {
            LoadedCommand = new RelayCommand<object?>(OnLoaded);
            NavigatePageCommand = new RelayCommand<PageViewModelBase>(NavigatePage);
            HomePageCommand = new RelayCommand(NavigateHomePage);
        }

        /// <summary>
        /// <see cref="FrameworkElement.Loaded"/> command.
        /// </summary>
        /// <param name="parameter">Add it if necessary.</param>
        protected virtual void OnLoaded(object? parameter)
        {
            Debug.WriteLine($"Loaded {this} page.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageViewModel"></param>
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