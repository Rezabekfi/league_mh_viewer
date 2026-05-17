using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using league_mh_viewer.ViewModels;
using league_mh_viewer.Views;
using league_mh_viewer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace league_mh_viewer;

public partial class App : Application
{
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }


  public override void OnFrameworkInitializationCompleted()
  { 
    var configuration = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
      .Build();

    var serviceCollection = new ServiceCollection();

    serviceCollection.AddSingleton<IConfiguration>(configuration);
    serviceCollection.AddSingleton<IRiotApiService, RiotApiService>();
    serviceCollection.AddSingleton<UserDataService>();
    serviceCollection.AddTransient<MainWindowViewModel>();

    var services = serviceCollection.BuildServiceProvider();

    var vm = services.GetRequiredService<MainWindowViewModel>();

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      desktop.MainWindow = new MainWindow
      {
          DataContext = vm
      };
    }

    base.OnFrameworkInitializationCompleted();
  }
}
