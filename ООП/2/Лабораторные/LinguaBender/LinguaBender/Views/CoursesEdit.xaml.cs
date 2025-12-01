using System.Windows;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using LinguaBender.Services;

namespace LinguaBender.Views
{
    public partial class CoursesEdit : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private string _userRole;

        public static RoutedCommand AddCommand { get; } = new();
        public static RoutedCommand EditCommand { get; } = new();
        public static RoutedCommand DeleteCommand { get; } = new();
        public static RoutedCommand RefreshCommand { get; } = new();



        public CoursesEdit(string userRole)
        {
            InitializeComponent();
            LoadCustomCursors();
            _userRole = userRole;
            ConfigureAccess();
            CommandBindings.Add(new CommandBinding(AddCommand, AddCourse));
            CommandBindings.Add(new CommandBinding(EditCommand, EditCourse));
            CommandBindings.Add(new CommandBinding(DeleteCommand, DeleteCourse));
            CommandBindings.Add(new CommandBinding(RefreshCommand, RefreshCourse));

        }

        public void AddCourse(object sender, RoutedEventArgs e)
        {
            var window = ((App)Application.Current)._serviceProvider.GetRequiredService<CoursesAddWindow>();
            window.Show();
        }
        public void EditCourse(object sender, RoutedEventArgs e)
        {

            CoursesEditWindow coursesEditWindow = new CoursesEditWindow();
            coursesEditWindow.Show();
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

            CoursesEdit coursesEdit = new CoursesEdit(_userRole)
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
            this.Close();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var homeWindow = new MainWindow(_userRole);
            homeWindow.Show();
            this.Close();
        }

        private void CoursesView_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = ((App)Application.Current)._serviceProvider;

            var courseService = serviceProvider.GetRequiredService<ICourseService>();
            var viewModel = new CoursesViewModel(courseService);

            CoursesView coursesView = new CoursesView(_userRole)
            {
                DataContext = viewModel
            };
            coursesView.Show();
            this.Close();
        }

        private void StudentsView_Click(object sender, RoutedEventArgs e)
        {
            StudentsView studentsView = new StudentsView(_userRole);
            studentsView.Show();
            this.Close();
        }

        private void StudentEdit_Click(object sender, RoutedEventArgs e)
        {
            StudentsEdit studentsEdit = new StudentsEdit(_userRole);
            studentsEdit.Show();
            this.Close();
        }
    }
}
