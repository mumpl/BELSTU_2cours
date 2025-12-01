using System.Windows;
using System.Windows.Input;
using System.IO;
using LinguaBender.ViewModels;
using LinguaBender.Data;
using Microsoft.Extensions.DependencyInjection;
using LinguaBender.Services;
using LinguaBender.Models;


namespace LinguaBender.Views
{
    public partial class CoursesView : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private string _userRole;
        private readonly User _currentUser;
        public CoursesView(User currentUser)
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var homeWindow = new MainWindow(_currentUser);
            homeWindow.Show();
            this.Close();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CoursesViewModel viewModel)
            {
                viewModel.ResetFilters();
            }
        }

        private void StudentsView_Click(object sender, RoutedEventArgs e)
        {
            StudentsView studentsView = new StudentsView(_currentUser);
            studentsView.Show();
            this.Close();
        }

        private void CourseEdit_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            // ✅ Вот здесь — получаем ICourseService из контейнера
            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService); //модель представления viewModel

            CoursesEdit coursesEdit = new CoursesEdit(_currentUser, serviceProvider)
            {
                DataContext = viewModel
            };
            coursesEdit.Show();
            this.Close();
        }

        private void StudentEdit_Click(object sender, RoutedEventArgs e)
        {
            StudentsEdit studentsEdit = new StudentsEdit(_currentUser);    
            studentsEdit.Show();
            this.Close();
        }
    }
}
