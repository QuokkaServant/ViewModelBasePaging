using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

namespace ViewModelBasePaging.ViewModels
{
    using Operation = ActionPageViewModel.Operation;

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
                case Key.End:
                    CurrentPageSource.NavigatePageCommand?.Execute(ViewModelLocator.Instance.EndPageViewModel);
                    break;
                case Key.OemPlus:
                    {
                        switch (CurrentPageSource) // Differentiate the behavior according to the base page.
                        {
                            case ActionPageViewModel actionPage:
                                actionPage.OperationCountCommand?.Execute(Operation.Increment);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Key.OemMinus:
                    {
                        if (CurrentPageSource is not ActionPageViewModel actionPage)
                            break;

                        actionPage.OperationCountCommand?.Execute(Operation.Decrement);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ReturnHomePage()
        {
            CurrentPageSource.NavigatePageCommand?.Execute(ViewModelLocator.Instance.HomePageViewModel);
        }
    }
}
