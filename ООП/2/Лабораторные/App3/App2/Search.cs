using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace App2
{
    public partial class Search : Form
    {
        private List<Student> students = new List<Student>();

        public Search()
        {
            InitializeComponent();
            LoadStudentsFromXml();
        }

        private void LoadStudentsFromXml()
        {
            if (File.Exists("students.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                using (TextReader reader = new StreamReader("students.xml"))
                {
                    students = (List<Student>)serializer.Deserialize(reader);
                }
            }
            else
            {
                MessageBox.Show("Файл students.xml не найден!");
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string query = searchTextBox.Text;
            listBoxResults.Items.Clear();

            foreach (Student student in students)
            {
                bool match = false;
                string studentData = $"{student.FullName} {student.BirthDate:dd.MM.yyyy} {student.Specialization} {student.Course} {student.Group} {student.AverageMark} {student.Gender} {student.WorkPlaceCompany} {student.WorkPlacePosition} {student.WorkPlaceExperience}";

                if (diapasonRadioButton.Checked)
                {
                    Regex dateRegex = new Regex(@"\b(202[3-4])\b");
                    if (dateRegex.IsMatch(student.BirthDate.ToString("yyyy")))
                    {
                        match = true;
                    }
                }
                else if (letterRadioButton.Checked)
                {
                    match = Regex.IsMatch(studentData, "ой", RegexOptions.IgnoreCase);
                }
                else
                {
                    match = studentData.Contains(query, StringComparison.OrdinalIgnoreCase);
                }

                if (match)
                {
                    listBoxResults.Items.Add($"{student.FullName}, {student.BirthDate:dd.MM.yyyy}, {student.Specialization}, Курс: {student.Course}, Группа: {student.Group}, Средний балл: {student.AverageMark}, Пол: {student.Gender}");
                }
            }
        }
        private void saveResultsButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
                saveFileDialog.Title = "Сохранить результаты поиска";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (var item in listBoxResults.Items)
                        {
                            writer.WriteLine(item.ToString());
                        }
                    }
                    MessageBox.Show("Результаты поиска успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void вперёдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortForm sortForm = new SortForm();
            sortForm.ShowDialog();
            this.Close();
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
