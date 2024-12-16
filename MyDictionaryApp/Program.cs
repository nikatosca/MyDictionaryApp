using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MyDictionaryApp.Services;
using MyDictionaryApp.ViewModels;
using MyDictionaryApp.Views;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MyDictionaryApp
{
    public class Program
    {
        [STAThread] // будем использовать однопоточную модель (это важно для графического интерфейса).
        public static void Main(string[] args)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Configuration file '{jsonFilePath}' was not found.");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Указывает базовый путь для файла конфигурации.
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // JSON файл как источник конфигурации.
                .Build();

            IServiceProvider serviceProvider = ConfigureServices(configuration); // Создаем провайдер зависимостей.
            BuildAvaloniaApp(serviceProvider).StartWithClassicDesktopLifetime(args); // Запуск Avalonia приложения.
        }

        public static IServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            // Регистрируем конфигурацию
            services.AddSingleton<IConfiguration>(configuration);

            // Регистрируем ApiService
            services.AddSingleton<ApiService>();  // Здесь регистрируем ApiService

            // Регистрируем ViewModels
            services.AddSingleton<AllWordsViewModel>();  
            services.AddSingleton<AddWordViewModel>();
            services.AddSingleton<DeleteWordViewModel>();
            services.AddSingleton<TestViewModel>();
            services.AddSingleton<WelcomeViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            // Регистрируем окно
            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }


        public static AppBuilder BuildAvaloniaApp(IServiceProvider serviceProvider)
            => AppBuilder.Configure<App>() // Настраивает экземпляр вашего приложения (`App`).
                .UsePlatformDetect() // Автоматически определяет платформу (Windows, MacOS, Linux).
                .WithInterFont() // Подключает шрифты.
                .LogToTrace() // Включает логирование.
                .AfterSetup(appBuilder => // Настраивает приложение после его создания.
                {
                    if (appBuilder.Instance is App app) 
                    {
                        app.ConfigureServices(serviceProvider); // Передает провайдер зависимостей в `App`.
                    }
                });
    }


}
