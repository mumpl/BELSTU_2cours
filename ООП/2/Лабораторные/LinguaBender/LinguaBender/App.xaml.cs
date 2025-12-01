using LinguaBender.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using LinguaBender.ViewModels;
using System.Globalization;
using System.Windows.Markup;
using LinguaBender.Views;
using LinguaBender.Services;

namespace LinguaBender
{

    public partial class App : Application
    {
        public readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=LAPTOP;Database=LINGUABENDER;Trusted_Connection=True;TrustServerCertificate=True;"), ServiceLifetime.Transient);

            //services.AddSingleton<CoursesViewModel>(); //один экз CoursesViewModel создан и исп-ся


            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<CoursesViewModel>();
            services.AddTransient<CoursesAddWindow>();

            // ✅ Строим провайдер ПОСЛЕ регистрации всех сервисов
            _serviceProvider = services.BuildServiceProvider(); //д получ сервисов AppDbContext и CoursesViewModel  Строим DI контейнер
        }

        public void ChangeLanguage(string cultureCode)
        {
            // Устанавливаем культуру для текущего потока
            var culture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Загружаем новый словарь ресурсов
            var dictionary = new ResourceDictionary();
            switch (cultureCode)
            {
                case "ru-RU":
                    dictionary.Source = new Uri("Resources/DictionaryRu.xaml", UriKind.Relative);
                    break;
                case "en-US":
                    dictionary.Source = new Uri("Resources/DictionaryEn.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("Resources/DictionaryRu.xaml", UriKind.Relative);
                    break;
            }

            // Очищаем старые словари и добавляем новый
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
