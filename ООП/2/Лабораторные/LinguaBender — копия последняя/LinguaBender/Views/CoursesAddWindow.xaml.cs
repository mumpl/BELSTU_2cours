using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.Models;
using System.IO;
using LinguaBender.UndoRedo;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender.Views
{
    public partial class CoursesAddWindow : Window
    {
        private AppDbContext _context;
        private Cursor _customCursor;
        private Cursor _customPointerCursor;
        private readonly IServiceProvider _serviceProvider;
        private readonly UndoRedoManager _undoRedoManager;
        public static RoutedCommand SaveCommand { get; } = new();
        public static RoutedCommand ResetCommand { get; } = new();
        public static RoutedCommand CloseCommand { get; } = new();
        public CoursesAddWindow(IServiceProvider serviceProvider, UndoRedoManager undoRedoManager)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _undoRedoManager = undoRedoManager;

            _context = serviceProvider.GetRequiredService<AppDbContext>(); // сохранил, если где-то ещё используешь
            LoadCustomCursors();

            CommandBindings.Add(new CommandBinding(SaveCommand, SaveCourse));
            CommandBindings.Add(new CommandBinding(ResetCommand, ResetCourse));
            CommandBindings.Add(new CommandBinding(CloseCommand, CloseWindow));

        }
        public void SaveCourse(object sender, RoutedEventArgs e)
        {
            try
            {
                Course course = new Course
                {
                    Course_Name = CourseName.Text,
                    Category = CourseCategory.Text,
                    Lessons = int.TryParse(LessonsQuantity.Text, out var lessonsQuantity) ? lessonsQuantity : 0,
                    Description = CourseDescription.Text,
                    Price = int.TryParse(CoursePrice.Text, out var price) ? price : 0,
                    Language = CourseLanguage.Text
                };

                var command = new AddCourseCommand(_serviceProvider, course);
                _undoRedoManager.ExecuteAction(command);

                MessageBox.Show("Курс добавлен с возможностью Undo!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении курса: {ex.Message}");
            }
        }
        public void ResetCourse(object sender, RoutedEventArgs e)
        {
            CourseName.Text = string.Empty;
            CourseCategory.Text = string.Empty;
            LessonsQuantity.Text = string.Empty;
            CourseDescription.Text = string.Empty;
            CoursePrice.Text = string.Empty;
            CourseLanguage.Text = string.Empty;
        }
        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
