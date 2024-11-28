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

        private ViewModelLocator()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddSingleton<HomePageViewModel>();
            serviceCollection.AddSingleton<EndPageViewModel>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private T GetRequiredService<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
    }
}
