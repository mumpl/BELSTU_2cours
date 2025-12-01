using System;
using System.Windows;
using ADO.NET.Models;
using ADO.NET.Data;

namespace ADO.NET
{
    public partial class CourseDialog : Window
    {

        private readonly bool isEditMode;
        private readonly string originalName; // Добавлено
        private readonly CourseRepository repo = new CourseRepository();

        public CourseDialog()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public CourseDialog(Course course)
        {
            InitializeComponent();
            isEditMode = true;

            txtName.Text = course.CourseName;
            txtCategory.Text = course.Category;
            txtLessons.Text = course.Lessons.ToString();
            txtDescription.Text = course.Description;
            txtPrice.Text = course.Price.ToString();

            originalName = course.CourseName;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var course = new Course
                {
                    CourseName = txtName.Text,
                    Category = txtCategory.Text,
                    Lessons = int.Parse(txtLessons.Text),
                    Description = txtDescription.Text,
                    Price = int.Parse(txtPrice.Text)
                };

                var repo = new CourseRepository();
                if (isEditMode)
                {
                    repo.UpdateCourse(course, originalName); // здесь важно передать originalName
                }
                else
                {
                    repo.AddCourse(course);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
