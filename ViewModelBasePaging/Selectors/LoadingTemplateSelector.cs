using System.Windows;

namespace ViewModelBasePaging.Selectors
{
    using Process = ViewModels.LoadingPageViewModel.Process;

    public class LoadingTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate? LoadingTemplate { get; set; }

        public DataTemplate? ComplatedTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case Process.Loading:
                    return LoadingTemplate;
                case Process.Complated:
                    return ComplatedTemplate;
                default:
                    break;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
