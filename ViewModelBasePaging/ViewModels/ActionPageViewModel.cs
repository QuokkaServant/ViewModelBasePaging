namespace ViewModelBasePaging.ViewModels
{
    public class ActionPageViewModel : PageViewModelBase
    {
        public enum Operation
        {
            Increment,

            Decrement
        }

        public int? Count
        {
            get => count;
            private set => SetProperty(ref count, value);
        }
        private int? count;

        public System.Windows.Input.ICommand OperationCountCommand { init; get; }

        public ActionPageViewModel()
        {
            OperationCountCommand = new CommunityToolkit.Mvvm.Input.RelayCommand<Operation>(OperationCount);
        }

        protected override void OnLoaded(object? parameter)
        {
            Count = int.TryParse(parameter as string, out int parsed) ? parsed : default;
            base.OnLoaded(parameter);
        }

        protected override void OnUnloaded()
        {
            //Count = null; // Initialize if necessary.
            base.OnUnloaded();
        }

        protected override void NavigatePage(PageViewModelBase? pageViewModel)
        {
            System.Diagnostics.Debug.WriteLine($"Current count is {Count}.");
            base.NavigatePage(pageViewModel);
        }

        private void OperationCount(Operation operation)
        {
            switch (operation)
            {
                case Operation.Increment:
                    Count++;
                    break;
                case Operation.Decrement:
                    Count--;
                    break;
                default:
                    break;
            }

            if (Count >= 0)
                return;

            base.NavigateHomePage(); // Call base function.
        }
    }
}
