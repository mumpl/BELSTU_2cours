using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace App2
{
    public partial class SortForm : Form
    {
        public SortForm()
        {
            InitializeComponent();
            LoadStudents();
        }
        private List<Student> students = new List<Student>();

        private void LoadStudents()
        {
            if (File.Exists("students.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                using (TextReader reader = new StreamReader("students.xml"))
                {
                    students = (List<Student>)serializer.Deserialize(reader);
                }
                DisplayStudents(students);
            }
            else
            {
                MessageBox.Show("Файл students.xml не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveSortedStudentsToXml(List<Student> sortedStudents)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (TextWriter writer = new StreamWriter("sorted_students.xml"))
            {
                serializer.Serialize(writer, sortedStudents);
            }
            MessageBox.Show("Сортированные данные сохранены в sorted_students.xml", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void DisplayStudents(List<Student> studentsList)
        {
            listBox1.Items.Clear();
            foreach (var student in studentsList)
            {
                listBox1.Items.Add($"{student.FullName}, {student.Course} курс, Средний балл: {student.AverageMark}");
            }
        }
        private void markRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (markRadioButton.Checked)
            {
                var sortedList = students.OrderByDescending(s => s.AverageMark).ToList();
                DisplayStudents(sortedList);
            }
        }

        private void nameRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (nameRadioButton.Checked)
            {
                var sortedList = students.OrderBy(s => s.FullName).ToList();
                DisplayStudents(sortedList);
            }
        }

        private void courseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (courseRadioButton.Checked)
            {
                var sortedList = students.OrderBy(s => s.Course).ToList();
                DisplayStudents(sortedList);
            }
        }

        private void SaveButton1_Click(object sender, EventArgs e)
        {
            if (nameRadioButton.Checked)
            {
                SaveSortedStudentsToXml(students.OrderBy(s => s.FullName).ToList());
            }
            else if (markRadioButton.Checked)
            {
                SaveSortedStudentsToXml(students.OrderByDescending(s => s.AverageMark).ToList());
            }
            else if (courseRadioButton.Checked)
            {
                SaveSortedStudentsToXml(students.OrderBy(s => s.Course).ToList());
            }
            else
            {
                MessageBox.Show("Выберите метод сортировки перед сохранением", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.ShowDialog();
            this.Close();
        }

        private void вперёдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutPr formAboutPr = new FormAboutPr();
            formAboutPr.ShowDialog();
            this.Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
