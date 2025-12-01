using System.Windows;
using System.Windows.Input;
using LinguaBender.Data;
using LinguaBender.Models;

namespace LinguaBender.Views
{
    public partial class CoursesEditWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly Course _course;

        public static RoutedCommand SaveCommand { get; } = new();
        public static RoutedCommand ResetCommand { get; } = new();
        public CoursesEditWindow(AppDbContext context, Course course)
        {
            InitializeComponent();

            _context = context;
            _course = course;

            LoadCourseData();

            CommandBindings.Add(new CommandBinding(SaveCommand, SaveCourse));
            CommandBindings.Add(new CommandBinding(ResetCommand, ResetCourse));
        }

        //НОВОЕ НОВОЕ НОВОЕ
        private void LoadCourseData()
        {
            CourseName.Text = _course.Course_Name;
            CourseCategory.Text = _course.Category;
            LessonsQuantity.Text = _course.Lessons.ToString();
            CourseDescription.Text = _course.Description;
            CoursePrice.Text = _course.Price.ToString();
            CourseLanguage.Text = _course.Language;
        }
        public void SaveCourse(object sender, RoutedEventArgs e)
        {
            try
            {
                _course.Course_Name = CourseName.Text;
                _course.Category = CourseCategory.Text;
                _course.Lessons = int.TryParse(LessonsQuantity.Text, out var lessonsQuantity) ? lessonsQuantity : 0;
                _course.Description = CourseDescription.Text;
                _course.Price = int.TryParse(CoursePrice.Text, out var price) ? price : 0;
                _course.Language = CourseLanguage.Text;

                _context.Courses.Update(_course);
                _context.SaveChanges();

                MessageBox.Show("Курс успешно обновлён!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении курса: {ex.Message}");
            }
        }
        public void ResetCourse(object sender, RoutedEventArgs e)
        {

        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
