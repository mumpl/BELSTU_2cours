using System.IO;
using System.Windows;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.Models;
using LinguaBender.Services;
using LinguaBender.ViewModels;
using LinguaBender.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender
{

    public partial class MainWindow : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private string _userRole;
        private readonly User _currentUser;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            LoadCustomCursors();
            _currentUser = currentUser;
            _userRole = currentUser.Role.RoleName;
            ConfigureAccess();
        }
        private void ConfigureAccess()
        {
            if (_userRole == "user")
            {
                StudentEdit.IsEnabled = false;
                CourseEdit.IsEnabled = false;
            }
        }

        private void LoadCustomCursors()
        {
            try
            {
                string cursorPath = @"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender\cursor.cur";
                string pointerPath = @"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender\cursorPointer.cur";

                if (File.Exists(cursorPath))
                {
                    _customCursor = new Cursor(cursorPath);
                }

                if (File.Exists(pointerPath))
                {
                    _customPointerCursor = new Cursor(pointerPath);
                }

                if (_customCursor != null)
                {
                    this.Cursor = _customCursor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсоров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_customPointerCursor != null)
            {
                this.Cursor = _customPointerCursor;
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_customCursor != null)
            {
                this.Cursor = _customCursor;
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void CoursesView_Click(object sender, RoutedEventArgs e)
        {                   //текущ экземп прил-я                       //экз контекста бд
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService); //модель представления viewModel

            CoursesView coursesView = new CoursesView(_currentUser)  //новое представление - визуал
            {
                DataContext = viewModel
            };
            coursesView.Show();
            this.Close();

        }

        private void PopCoursesView_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService);

            CoursesView coursesView = new CoursesView(_currentUser)
            {
                DataContext = viewModel
            };
            coursesView.Show();
            this.Close();
        }

        private void CourseEdit_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            //  Вот здесь — получаем ICourseService из контейнера
            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService); //модель представления viewModel

            CoursesEdit coursesEdit = new CoursesEdit(_currentUser, serviceProvider)
            {
                DataContext = viewModel
            };
            coursesEdit.Show();
            this.Close();
        }

        private void StudentsView_Click(object sender, RoutedEventArgs e)
        {
            StudentsView studentsView = new StudentsView(_currentUser);
            studentsView.Show();
            this.Close();
        }

        private void StudentEdit_Click(object sender, RoutedEventArgs e)
        {
            StudentsEdit studentsEdit = new StudentsEdit(_currentUser);
            studentsEdit.Show();
            this.Close();
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ToggleTheme();
        }

        private void ProfileWindow_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;
            var userService = serviceProvider.GetRequiredService<IUserService>();

            var profileWindow = new ProfileWindow(userService, _currentUser);
            profileWindow.Show();
        }

    }
}