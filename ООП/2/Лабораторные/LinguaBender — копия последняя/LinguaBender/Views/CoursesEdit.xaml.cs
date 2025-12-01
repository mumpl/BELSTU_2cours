using System.Windows;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using LinguaBender.Services;
using LinguaBender.Models;
using Microsoft.EntityFrameworkCore;
using LinguaBender.UndoRedo;
using System.Windows.Controls;

namespace LinguaBender.Views
{
    public partial class CoursesEdit : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private string _userRole;
        private readonly User _currentUser;

        private readonly UndoRedoManager _undoRedoManager;
        private readonly IServiceProvider _serviceProvider;

        public static RoutedCommand AddCommand { get; } = new();
        public static RoutedCommand EditCommand { get; } = new();
        public static RoutedCommand DeleteCommand { get; } = new();
        public static RoutedCommand RefreshCommand { get; } = new();



        public CoursesEdit(User currentUser, IServiceProvider serviceProvider)
        {
            InitializeComponent();


            _serviceProvider = serviceProvider;
            _undoRedoManager = _serviceProvider.GetRequiredService<UndoRedoManager>();

            LoadCustomCursors();

            _currentUser = currentUser;
            _userRole = currentUser.Role.RoleName;

            ConfigureAccess();

            CommandBindings.Add(new CommandBinding(AddCommand, AddCourse));
            CommandBindings.Add(new CommandBinding(EditCommand, EditCourse));
            CommandBindings.Add(new CommandBinding(DeleteCommand, DeleteCourse));
            CommandBindings.Add(new CommandBinding(RefreshCommand, RefreshCourse));
        }

        public void AddCourse(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;
            var undoRedoManager = serviceProvider.GetRequiredService<UndoRedoManager>();

            CoursesAddWindow window = new CoursesAddWindow(serviceProvider, undoRedoManager);
            window.Show();

        }
        public void EditCourse(object sender, RoutedEventArgs e)
        {
            if (DataContext is CoursesViewModel viewModel && viewModel.SelectedCourse != null)
            {
                var dbContext = ((App)Application.Current)._serviceProvider.GetRequiredService<AppDbContext>();
                var editWindow = new CoursesEditWindow(dbContext, viewModel.SelectedCourse);
                editWindow.ShowDialog();

                RefreshCourse(null, null);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите курс для редактирования.");
            }
        }
        public void DeleteCourse(object sender, RoutedEventArgs e)
        {
            string CourseNameToDelete = Microsoft.VisualBasic.Interaction.InputBox("Введите название курса для удаления: ", "Удаление курса", "");
            if (string.IsNullOrWhiteSpace(CourseNameToDelete))
            {
                MessageBox.Show("название курса для удаления не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var dbContext = ((App)Application.Current)._serviceProvider.GetRequiredService<AppDbContext>())
            {
                var courseToDelete = dbContext.Courses.FirstOrDefault(c => c.Course_Name.ToLower() == CourseNameToDelete.ToLower());
                if (courseToDelete != null)
                {
                    dbContext.Courses.Remove(courseToDelete);
                    dbContext.SaveChanges();
                    MessageBox.Show($"Курс {CourseNameToDelete} успешно удалён!", "Успех", MessageBoxButton.OK , MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Курс с таким названием не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void RefreshCourse(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;
            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService);

            var coursesEdit = new CoursesEdit(_currentUser, serviceProvider)
            {
                DataContext = viewModel
            };

            coursesEdit.Show();
            this.Close();
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



        // НОВОЕ НОВОЕ НОВОЕ
        private void InfoCard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Course selectedCourse)
            {
                var dbContext = ((App)Application.Current)._serviceProvider.GetRequiredService<AppDbContext>();
                var editWindow = new CoursesEditWindow(dbContext, selectedCourse);
                editWindow.ShowDialog();

                RefreshCourse(null, null); // обновим список после закрытия
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.Undo();
            RefreshCourse(null, null);
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.Redo();
            RefreshCourse(null, null);
        }

    }
}
