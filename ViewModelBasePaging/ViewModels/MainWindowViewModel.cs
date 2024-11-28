using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

namespace ViewModelBasePaging.ViewModels
{
    public sealed class MainWindowViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public PageViewModelBase CurrentPageSource
        {
            get => currentPageSource;
            set
            {
                SetProperty(ref currentPageSource, value);
                OnPropertyChanged(nameof(IsCurrentHomePage));
            }
        }
        private PageViewModelBase currentPageSource = ViewModelLocator.Instance.HomePageViewModel;

        public bool IsCurrentHomePage => CurrentPageSource == ViewModelLocator.Instance.HomePageViewModel;

        public ICommand PreviewKeyDownCommand { init; get; }

        public ICommand HomePageCommand { init; get; }

        public MainWindowViewModel()
        {
            PreviewKeyDownCommand = new RelayCommand<KeyEventArgs>(OnPreviewKeyDown);
            HomePageCommand = new RelayCommand(ReturnHomePage);
        }

        private void OnPreviewKeyDown(KeyEventArgs? args)
        {
            switch (args?.Key)
            {
                case Key.Home:
                    ReturnHomePage();
                    break;
                default:
                    break;
            }
        }

        private void ReturnHomePage()
        {
            CurrentPageSource = ViewModelLocator.Instance.HomePageViewModel;
        }
    }
}
