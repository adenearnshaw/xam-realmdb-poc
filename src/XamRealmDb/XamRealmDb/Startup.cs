using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Shiny;
using System;
using XamRealmDb.Api;
using XamRealmDb.Data;
using XamRealmDb.Managers;
using XamRealmDb.ViewModels;

namespace XamRealmDb;
public class Startup : ShinyStartup
{
    public override void ConfigureServices(IServiceCollection services, IPlatform platform)
    {
        services.AddTransient<ApiClient>();

        services.AddSingleton<ItemRepository>();

        services.AddSingleton<ItemsManager>();

        services.AddTransient<MainViewModel>();

        //services.RegisterJob(RefreshDataJob.JobInfo);

        services.AddSingleton<RefreshDataJob>();
    }

    public override IServiceProvider CreateServiceProvider(IServiceCollection services)
    {
        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        return Ioc.Default;
    }
}
