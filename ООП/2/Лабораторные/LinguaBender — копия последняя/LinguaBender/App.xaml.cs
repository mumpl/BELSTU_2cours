using LinguaBender.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using LinguaBender.ViewModels;
using System.Globalization;
using System.Windows.Markup;
using LinguaBender.Views;
using LinguaBender.Services;
using LinguaBender.UndoRedo;
using LinguaBender.Data.UnitOfWork;

namespace LinguaBender
{
    public partial class App : Application
    {
        public readonly IServiceProvider _serviceProvider;
        public string _currentTheme = "Light";

        public App()
        {
            var services = new ServiceCollection();

            

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=LAPTOP;Database=LINGUABENDER;Trusted_Connection=True;TrustServerCertificate=True;"),
                ServiceLifetime.Transient);

            // NEW NEW NEW NEW NEW
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // NEW NEW NEW NEW NEW


            services.AddTransient<ICourseService, CourseService>();

            services.AddSingleton<UndoRedoManager>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<RegisterWindow>();
            services.AddTransient<ProfileWindow>();

            services.AddTransient<CoursesViewModel>();
            services.AddTransient<CoursesAddWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Загружаем тему только здесь, когда все ресурсы готовы
            LoadTheme("Light");

            //Загружаем язык 
            ChangeLanguage("ru-RU");
        }

        public void LoadTheme(string themeName)
        {
            var themeDict = new ResourceDictionary();

            switch (themeName)
            {
                case "Light":
                    themeDict.Source = new Uri("Resources/LightTheme.xaml", UriKind.Relative);
                    _currentTheme = "Light";
                    break;
                case "Rose":
                    themeDict.Source = new Uri("Resources/RoseTheme.xaml", UriKind.Relative);
                    _currentTheme = "Rose";
                    break;
            }

            // Удаляем предыдущие темы
            var dictionaries = Current.Resources.MergedDictionaries;
            for (int i = dictionaries.Count - 1; i >= 0; i--)
            {
                var uri = dictionaries[i].Source?.OriginalString;
                if (uri != null && (uri.Contains("LightTheme.xaml") || uri.Contains("RoseTheme.xaml")))
                {
                    dictionaries.RemoveAt(i);
                }
            }

            dictionaries.Add(themeDict);
        }

        public void ToggleTheme()
        {
            var newTheme = _currentTheme == "Light" ? "Rose" : "Light";
            LoadTheme(newTheme);
        }

        public void ChangeLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

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

            // Удаляем ВСЕ кроме темы!
            var dictionaries = Application.Current.Resources.MergedDictionaries;
            for (int i = dictionaries.Count - 1; i >= 0; i--)
            {
                var uri = dictionaries[i].Source?.OriginalString;
                if (uri != null && (uri.Contains("DictionaryRu.xaml") || uri.Contains("DictionaryEn.xaml")))
                {
                    dictionaries.RemoveAt(i);
                }
            }

            dictionaries.Add(dictionary);
        }
    }
}
