using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinMvvm;
using ThinMvvm.Transition;

namespace AdventCalender2023;

public partial class App : ThinApplication
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        IConfiguration configuration = new ConfigurationBuilder()
            .Add(new PrefixJsonConfigurationSource() { Path = "messages.json", Prefix = "messages", ReloadOnChange = true, })
            .Add(new PrefixJsonConfigurationSource() { Path = "configs.json", Prefix = "configs", ReloadOnChange = true, })
            .Build();

        services
            .AddSingleton(configuration)
            .ConfigureTransition(builder =>
        {
            builder.AddSingletonWindow<MainWindow, MainWindowViewModel>("mainWindow");
        });
    }

    protected override void PostStartup(IServiceProvider provider)
    {
        base.PostStartup(provider);

        ITransitionManager transitionManager = provider.GetRequiredService<ITransitionManager>();
        transitionManager.ShowWindow("mainWindow");
    }
}

