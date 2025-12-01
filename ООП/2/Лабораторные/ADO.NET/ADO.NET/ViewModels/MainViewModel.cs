using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO.NET.Data;
using ADO.NET.Models;
using System.Windows.Input;
using System.Windows;


namespace ADO.NET.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CourseRepository _courseRepository;
        private readonly UserRepository _userRepository;

        private User _currentUser;
        private ObservableCollection<Course> _courses;
        private ObservableCollection<User> _users;
        private string _selectedCategory;
        private string _searchText;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            DatabaseHelper.EnsureDatabaseExists();
            _courseRepository = new CourseRepository();
            _userRepository = new UserRepository();

            Courses = new ObservableCollection<Course>(_courseRepository.GetAllCourses());
            Users = new ObservableCollection<User>(_userRepository.GetAllUsers());

            LoginCommand = new RelayCommand(Login);
            LogoutCommand = new RelayCommand(Logout);
            AddCourseCommand = new RelayCommand(AddCourse);
            UpdateCourseCommand = new RelayCommand(UpdateCourse);
            DeleteCourseCommand = new RelayCommand(DeleteCourse);
            AddUserCommand = new RelayCommand(AddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            FilterCoursesCommand = new RelayCommand(FilterCourses);
            SearchCoursesCommand = new RelayCommand(SearchCourses);
        }

        public ObservableCollection<Course> Courses
        {
            get => _courses;
            set
            {
                _courses = value;
                OnPropertyChanged(nameof(Courses));
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(IsAdmin));
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public bool IsAdmin => CurrentUser?.RoleId == 1;
        public bool IsLoggedIn => CurrentUser != null;

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public List<string> Categories => new List<string> { "Все", "Базовый", "Средний", "Продвинутый" };

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand AddCourseCommand { get; }
        public ICommand UpdateCourseCommand { get; }
        public ICommand DeleteCourseCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand FilterCoursesCommand { get; }
        public ICommand SearchCoursesCommand { get; }

        private void Login(object parameter)
        {
            if (parameter is object[] values && values.Length == 2)
            {
                string username = values[0] as string;
                string password = values[1] as string;

                CurrentUser = _userRepository.Authenticate(username, password);

                if (CurrentUser == null)
                {
                    MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Logout(object parameter)
        {
            CurrentUser = null;
        }

        private void AddCourse(object parameter)
        {
            if (parameter is Course course)
            {
                try
                {
                    _courseRepository.AddCourse(course);
                    Courses.Add(course);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении курса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateCourse(object parameter)
        {
            if (parameter is CourseUpdateModel updateModel)
            {
                try
                {
                    _courseRepository.UpdateCourse(updateModel.UpdatedCourse, updateModel.OriginalName);

                    var existingCourse = Courses.FirstOrDefault(c => c.CourseName == updateModel.OriginalName);
                    if (existingCourse != null)
                    {
                        var index = Courses.IndexOf(existingCourse);
                        Courses[index] = updateModel.UpdatedCourse;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении курса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void DeleteCourse(object parameter)
        {
            if (parameter is string courseName)
            {
                try
                {
                    _courseRepository.DeleteCourse(courseName);
                    var course = Courses.FirstOrDefault(c => c.CourseName == courseName);
                    if (course != null)
                    {
                        Courses.Remove(course);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении курса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddUser(object parameter)
        {
            if (parameter is User user)
            {
                try
                {
                    _userRepository.AddUser(user);
                    Users.Add(user);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateUser(object parameter)
        {
            if (parameter is User user)
            {
                try
                {
                    _userRepository.UpdateUser(user);
                    var existingUser = Users.FirstOrDefault(u => u.Id == user.Id);
                    if (existingUser != null)
                    {
                        var index = Users.IndexOf(existingUser);
                        Users[index] = user;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteUser(object parameter)
        {
            if (parameter is int userId)
            {
                try
                {
                    _userRepository.DeleteUser(userId);
                    var user = Users.FirstOrDefault(u => u.Id == userId);
                    if (user != null)
                    {
                        Users.Remove(user);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FilterCourses(object parameter)
        {
            if (SelectedCategory == "Все")
            {
                Courses = new ObservableCollection<Course>(_courseRepository.GetAllCourses());
            }
            else
            {
                Courses = new ObservableCollection<Course>(_courseRepository.GetCoursesByCategory(SelectedCategory));
            }
        }

        private void SearchCourses(object parameter)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Courses = new ObservableCollection<Course>(_courseRepository.GetAllCourses());
            }
            else
            {
                var allCourses = _courseRepository.GetAllCourses();
                var filtered = allCourses.Where(c =>
                    c.CourseName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                Courses = new ObservableCollection<Course>(filtered);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
