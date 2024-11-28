namespace ViewModelBasePaging.ViewModels
{
    public class HomePageViewModel : PageViewModelBase
    {
        protected override void NavigatePage(PageViewModelBase? pageViewModel)
        {
            switch (pageViewModel)
            {
                case EndPageViewModel:
                    System.Diagnostics.Debug.WriteLine("Go directly from the home page to the end page.");
                    break;
                default:
                    break;
            }
            base.NavigatePage(pageViewModel);
        }
    }
}
