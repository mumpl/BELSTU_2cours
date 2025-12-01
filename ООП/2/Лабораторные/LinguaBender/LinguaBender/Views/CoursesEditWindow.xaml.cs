using System.Windows;
using System.Windows.Input;

namespace LinguaBender.Views
{
    public partial class CoursesEditWindow : Window
    {

        public static RoutedCommand SaveCommand { get; } = new();
        public static RoutedCommand ResetCommand { get; } = new();
        public CoursesEditWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(SaveCommand, SaveCourse));
            CommandBindings.Add(new CommandBinding(ResetCommand, ResetCourse));
        }
        public void SaveCourse(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Курс добавлен!");
        }
        public void ResetCourse(object sender, RoutedEventArgs e)
        {

        }
    }
}
