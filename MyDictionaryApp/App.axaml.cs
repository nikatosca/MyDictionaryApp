using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MyDictionaryApp.Services;
using MyDictionaryApp.ViewModels;
using MyDictionaryApp.Views;
namespace MyDictionaryApp;

public partial class App : Application
{
    private IServiceProvider _serviceProvider; //Хранит провайдер зависимостей.

    //Принимает провайдер зависимостей, чтобы App мог использовать зарегистрированные сервисы
    public void ConfigureServices(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    
    //установка Главного окна единственного
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
