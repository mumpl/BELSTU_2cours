using System.IO;
using System.Windows;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.Models;
using LinguaBender.Services;
using LinguaBender.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender.Views
{
    public partial class StudentsView : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private string _userRole;
        private readonly User _currentUser;
        public StudentsView(User currentUser)
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

        private void CoursesView_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            // ✅ Вот здесь — получаем ICourseService из контейнера
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


        private void TriggerButton_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата! Это реакция на событие.", "Триггер", MessageBoxButton.OK, MessageBoxImage.Information);

            // Лог в файл
            File.AppendAllText("log.txt", $"{DateTime.Now}: TriggerButton was clicked.\n");
        }

        private void TriggerButton2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы нажали на кнопку с DataTrigger!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            TriggerButton2.Content = "";
        }
    }
}
