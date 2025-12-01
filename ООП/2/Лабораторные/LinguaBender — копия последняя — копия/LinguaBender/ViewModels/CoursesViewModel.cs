using LinguaBender.Data;
using LinguaBender.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using LinguaBender.Views;
using LinguaBender.ViewModels;
using CommunityToolkit.Mvvm.Input;
using LinguaBender.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinguaBender.ViewModels
{
    public class CoursesViewModel : ViewModelBase
    {
        //private readonly AppDbContext _dbContext; //экз контекста бд
        private readonly ICourseService _courseService;

        /*ДЛЯ СОРТИРОВКИ*/
        public ObservableCollection<Course> Courses { get; set; } = new(); //автомат увед UI о  изм-и эл-в 
        public ObservableCollection<Course> FilteredCourses { get; set; } = new(); // Отфильтрованные курсы
        public List<string> Categories { get; set; } = new() { "Все", "Базовый", "Средний", "Продвинутый" };
        public List<string> Languages { get; set; } = new() { "Все", "Азиатский", "Арабский", "Европейский" };
        public List<int> PriceOptions { get; set; } = new() { 0, 500, 1000, 1500, 2000, 2500, 3000, 3500 };

        //НОВЫЙ НОВЫЙ НОВЫЙ
        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        //НОВОЕ НОВОЕ

        private int _minPrice = 0;
        public int MinPrice
        {
            get => _minPrice;
            set
            {
                if (value <= MaxPrice)
                {
                    _minPrice = value;
                    OnPropertyChanged();
                    ApplyFilters();
                }
            }
        }

        private int _maxPrice = 3500;
        public int MaxPrice
        {
            get => _maxPrice;
            set
            {
                if (value >= MinPrice)
                {
                    _maxPrice = value;
                    OnPropertyChanged();
                    ApplyFilters();
                }
            }
        }

        private string _selectedCategory = "Все";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        private string _selectedLanguage = "Все";
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        public CoursesViewModel(ICourseService courseService)
        {
            _courseService = courseService;
            LoadCourses();
        }

        private async void LoadCourses()
        {
            var coursesFromDb = await _courseService.GetAllCoursesAsync();
            Courses.Clear();//чтоб не было дублир-я
            foreach (var course in coursesFromDb)
            {
                Courses.Add(course);
            }
            ApplyFilters();
            
        }
        public void ApplyFilters()
        {
            var filtered = Courses.Where(c =>
                c.Price >= MinPrice &&
                c.Price <= MaxPrice &&
                (SelectedCategory == "Все" || c.Category == SelectedCategory) &&
                (SelectedLanguage == "Все" || c.Language == SelectedLanguage) &&
                (string.IsNullOrWhiteSpace(SearchText) ||
             c.Course_Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
             c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            FilteredCourses.Clear();
            foreach (var course in filtered)
            {
                FilteredCourses.Add(course);
            }

            OnPropertyChanged(nameof(FilteredCourses));
        }

        public void ResetFilters()
        {
            MinPrice = 0;
            MaxPrice = 3500;
            SelectedCategory = "Все";
            SelectedLanguage = "Все";
            SearchText = string.Empty;

            ApplyFilters();
        }
    }
}
