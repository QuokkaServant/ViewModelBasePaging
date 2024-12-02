using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ViewModelBasePaging.Behaviors
{
    using ViewModels;

    public class NavigationFrameBehavior : Microsoft.Xaml.Behaviors.Behavior<Frame>
    {
        public static readonly DependencyProperty CurrentPageSourceProperty =
            DependencyProperty.Register(nameof(CurrentPageSource), typeof(PageViewModelBase), typeof(NavigationFrameBehavior), new PropertyMetadata(null));

        public PageViewModelBase CurrentPageSource
        {
            get => (PageViewModelBase)GetValue(CurrentPageSourceProperty);
            set => SetValue(CurrentPageSourceProperty, value);
        }

        protected override void OnAttached()
        {
            base.AssociatedObject.Navigated += NavigatedAssociatedObject;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.Navigated -= NavigatedAssociatedObject;
            base.OnDetaching();
        }


        private void NavigatedAssociatedObject(object sender, NavigationEventArgs e)
        {
            if (!base.AssociatedObject.CanGoBack || CurrentPageSource is not HomePageViewModel)
                return;

            // Run the code if you need to delete the existing paging cache when you return to the home page.

            do
            {
                base.AssociatedObject.NavigationService.RemoveBackEntry();
            }
            while (base.AssociatedObject.CanGoBack);
        }

    }
}
