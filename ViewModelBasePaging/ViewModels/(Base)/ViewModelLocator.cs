using Microsoft.Extensions.DependencyInjection;

namespace ViewModelBasePaging.ViewModels
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider serviceProvider;

        public static ViewModelLocator Instance
        {
            get
            {
                if (instance == null || !instance.IsValueCreated)
                    instance = new Lazy<ViewModelLocator>(() => new ViewModelLocator());

                return instance.Value;
            }
        }
        private static Lazy<ViewModelLocator>? instance;

        public MainWindowViewModel MainWindowViewModel => GetRequiredService<MainWindowViewModel>();
        
        public HomePageViewModel HomePageViewModel => GetRequiredService<HomePageViewModel>();

        public EndPageViewModel EndPageViewModel => GetRequiredService<EndPageViewModel>();

        public ActionPageViewModel ActionPageViewModel => GetRequiredService<ActionPageViewModel>();

        private ViewModelLocator()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddSingleton<HomePageViewModel>();
            serviceCollection.AddSingleton<EndPageViewModel>();

            serviceCollection.AddSingleton<ActionPageViewModel>();

            // TODO : Add to the collection when you add a page.

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private T GetRequiredService<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
    }
}
