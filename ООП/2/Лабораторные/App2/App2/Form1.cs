using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace App2
{

    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();

        public event EventHandler<string> ValidationError;
        protected virtual void OnValidationError(string message)
        {
            ValidationError?.Invoke(this, message);
        }

        public Form1()
        {
            InitializeComponent();
            trackMark.Scroll += (sender, eventArgs) => trackMark.Text = trackMark.Value.ToString();
            dataGridViewStudents.DataSource = students;
        }

        public bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                OnValidationError("имя не может быть пустым");
                return false;
            }
            return true;
        }
        private bool ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate >= DateTime.Now)
            {
                OnValidationError("Дата рождения не может быть в будущем.");
                return false;
            }
            return true;
        }
        private bool ValidateSpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
            {
                OnValidationError("Выберите специальность.");
                return false;
            }
            return true;
        }

        private bool ValidateGroup(int group)
        {
            if (group < 1 || group > 10)
            {
                OnValidationError("Группа должна быть в пределах от 1 до 10.");
                return false;
            }
            return true;
        }

        private bool ValidateCourse(int course)
        {
            if (course < 1 || course > 4)
            {
                OnValidationError("Курс должен быть от 1 до 4.");
                return false;
            }
            return true;
        }

        private bool ValidateAverageMark(int mark)
        {
            if (mark < 1 || mark > 10)
            {
                OnValidationError("Средний балл должен быть в пределах от 1 до 10.");
                return false;
            }
            return true;
        }

        private bool ValidateGender(string gender)
        {
            if (gender != "M" && gender != "Ж")
            {
                OnValidationError("Пол должен быть 'M' или 'Ж'.");
                return false;
            }
            return true;
        }
        private void button_save_xml_Click(object sender, EventArgs e)
        {
            string fullName = surname.Text + " " + name.Text + " " + lastname.Text;
            DateTime birthDate = monthCalendar1.SelectionStart;
            string specialization = comboSpecialization.SelectedItem?.ToString();
            int group = int.Parse(comboGroup.SelectedItem?.ToString() ?? "0");
            int averageMark = trackMark.Value;
            string gender = rbMale.Checked ? "M" : "Ж";

            if (!ValidateFullName(fullName) || !ValidateBirthDate(birthDate) || !ValidateSpecialization(specialization) ||
                !ValidateGroup(group) || !ValidateCourse(rb1.Checked ? 1 : rb2.Checked ? 2 : rb3.Checked ? 3 : 4) ||
                !ValidateAverageMark(averageMark) || !ValidateGender(gender))
            {
                return;
            }

            Student student = new Student()
            {
                FullName = fullName,
                BirthDate = birthDate,
                Specialization = specialization,
                Course = rb1.Checked ? 1 : rb2.Checked ? 2 : rb3.Checked ? 3 : 4,
                Group = group,
                AverageMark = averageMark,
                Gender = gender,
            };

            if (!string.IsNullOrWhiteSpace(company.Text) && !string.IsNullOrWhiteSpace(position.Text))
            {
                student.WorkPlace = new WorkPlace()
                {
                    Company = company.Text,
                    Position = position.Text,
                    Experience = int.Parse(trackExperience.Text)
                };
            }

            students.Add(student);
            dataGridViewStudents.DataSource = null;
            dataGridViewStudents.DataSource = students;
            SaveToXml();
            saveResult.Text = "Данные студента сохранены успешно!";
        }
        private void SaveToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (TextWriter writer = new StreamWriter("students.xml"))
            {
                serializer.Serialize(writer, students);
            }
        }
        private void button_writeData_Click(object sender, EventArgs e)
        {
            if (File.Exists("students.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                using (TextReader reader = new StreamReader("students.xml"))
                {
                    students = (List<Student>)serializer.Deserialize(reader);
                }
                dataGridViewStudents.DataSource = null;
                dataGridViewStudents.DataSource = students;
                dataResult.Text = "Данные успешно выведены из XML";
            }
            else
            {
                dataResult.Text = "Файл students.xml не найден!";
            }
        }
        private void button_budget_Click(object sender, EventArgs e)
        {
            double budget = students.Count * 1000;
            budget_result.Text = budget.ToString() + " рублей";

        }
        private void ValidationErrorHandler(object sender, string message)
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ValidationError += ValidationErrorHandler;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void budget_result_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            Close();
        }
    }
    public class Student
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Specialization { get; set; }
        public int Course {get; set;}
        public int Group { get; set; }
        public int AverageMark { get; set; }
        public string Gender { get; set; }
        public string WorkPlaceCompany => WorkPlace?.Company;
        public string WorkPlacePosition => WorkPlace?.Position;
        public int? WorkPlaceExperience => WorkPlace?.Experience;
        public WorkPlace WorkPlace { get; set; }
    }

    public class WorkPlace
    {
        public string Company { get; set; }
        public string Position { get; set; }
        public int Experience { get; set; }
    }

}
