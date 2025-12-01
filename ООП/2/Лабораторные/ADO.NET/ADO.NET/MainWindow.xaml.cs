using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ADO.NET.Data;
using ADO.NET.Models;

namespace ADO.NET
{
    public partial class MainWindow : Window
    {
        private readonly CourseRepository courseRepo = new CourseRepository();
        private readonly UserRepository userRepo = new UserRepository();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Load;

            BtnAdd.Click += BtnAdd_Click;
            BtnEdit.Click += BtnEdit_Click;
            BtnDelete.Click += BtnDelete_Click;
            
        }

        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            DatabaseHelper.EnsureDatabaseExists();
            RefreshData();
        }

        private void RefreshData()
        {
            var allCourses = courseRepo.GetAllCourses();

            if (CategoryFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedCategory = selectedItem.Content.ToString();
                if (selectedCategory != "Все")
                    allCourses = allCourses.Where(c => c.Category == selectedCategory).ToList();
            }

            CoursesGrid.ItemsSource = allCourses;
            UsersGrid.ItemsSource = userRepo.GetAllUsers();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                var dialog = new CourseDialog();
                if (dialog.ShowDialog() == true)
                    RefreshData();
            }
            else
            {
                var dialog = new UserDialog();
                if (dialog.ShowDialog() == true)
                    RefreshData();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedIndex == 0 && CoursesGrid.SelectedItem is Course course)
            {
                var dialog = new CourseDialog(course);
                if (dialog.ShowDialog() == true)
                    RefreshData();
            }
            else if (UsersGrid.SelectedItem is User user)
            {
                var dialog = new UserDialog(user);
                if (dialog.ShowDialog() == true)
                    RefreshData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedIndex == 0 && CoursesGrid.SelectedItem is Course course)
            {
                courseRepo.DeleteCourse(course.CourseName);
                RefreshData();
            }
            else if (UsersGrid.SelectedItem is User user)
            {
                userRepo.DeleteUser(user.Id);
                RefreshData();
            }
        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            if (TabControl.SelectedIndex == 0)
            {
                var sorted = new List<Course>(courseRepo.GetAllCourses());
                sorted.Sort((a, b) => string.Compare(a.CourseName, b.CourseName));
                CoursesGrid.ItemsSource = sorted;
            }
            else
            {
                var sorted = new List<User>(userRepo.GetAllUsers());
                sorted.Sort((a, b) => string.Compare(a.Name, b.Name));
                UsersGrid.ItemsSource = sorted;
            }
        }

        
        private void CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedCategory = selectedItem.Content.ToString();
                List<Course> allCourses = courseRepo.GetAllCourses();

                if (selectedCategory != "Все")
                {
                    var filtered = allCourses.Where(c => c.Category == selectedCategory).ToList();
                    CoursesGrid.ItemsSource = filtered;
                }
                else
                {
                    CoursesGrid.ItemsSource = allCourses;
                }
            }
        }




    }
}