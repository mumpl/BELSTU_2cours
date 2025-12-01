using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ValidationError += ValidationErrorHandler;
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
        private void button_save_xml_Click(object sender, EventArgs e)
        {
            Student student = new Student()
            {
                FullName = surname.Text + " " + name.Text + " " + lastname.Text,
                BirthDate = monthCalendar1.SelectionStart,
                Specialization = comboSpecialization.SelectedItem?.ToString(),
                Course = rb1.Checked ? 1 : rb2.Checked ? 2 : rb3.Checked ? 3 : 4,
                Group = int.TryParse(comboGroup.SelectedItem?.ToString(), out int group) ? group : 0,
                AverageMark = trackMark.Value,
                Gender = rbMale.Checked ? "M" : "Ж",
            };

            if (!string.IsNullOrWhiteSpace(company.Text) && !string.IsNullOrWhiteSpace(position.Text))
            {
                student.WorkPlace = new WorkPlace()
                {
                    Company = company.Text,
                    Position = position.Text,
                    Experience = int.TryParse(trackExperience.Text, out int exp) ? exp : 0
                };
            }

            var context = new ValidationContext(student);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(student, context, results, true))
            {
                foreach (var error in results)
                {
                    OnValidationError(error.ErrorMessage);
                }
                return;
            }

            students.Add(student);
            dataGridViewStudents.DataSource = null;
            dataGridViewStudents.DataSource = students;
            SaveToXml();
            toolStripStatusLabel.Text = $"Студент добавлен. Всего студентов: {students.Count}, Действие: Сохранение данных, Время: {DateTime.Now}";
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
                toolStripStatusLabel.Text = $"Данные загружены. Всего студентов: {students.Count}, Действие: Загрузка данных, Время: {DateTime.Now}";
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
        private void button_clear_Click(object sender, EventArgs e)
        {
            surname.Clear();
            name.Clear();
            lastname.Clear();
            company.Clear();
            position.Clear();
            trackExperience.Clear();

            monthCalendar1.SelectionStart = DateTime.Now;

            comboSpecialization.SelectedIndex = -1;
            comboGroup.SelectedIndex = -1;

            rbMale.Checked = false;
            rbFemale.Checked = false;

            trackMark.Value = trackMark.Minimum;

            students.Clear();
            dataGridViewStudents.DataSource = null;

            saveResult.Text = string.Empty;
            dataResult.Text = string.Empty;
            budget_result.Text = string.Empty;

            toolStripStatusLabel.Text = "Все данные очищены, Время: " + DateTime.Now;
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
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAboutPr infoform = new FormAboutPr();
            infoform.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menuStrip1.Visible = !menuStrip1.Visible;
        }

        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Search searchForm = new Search();
            searchForm.ShowDialog();
        }

        private void сортировкаПоToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortForm sortForm = new SortForm();
            sortForm.ShowDialog();
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_clear_Click(sender, e);
        }

        private void вперёдToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string surnameToDelete = Microsoft.VisualBasic.Interaction.InputBox("Введите фамилию студента для удаления:", "Удаление студента", "");
            if (string.IsNullOrWhiteSpace(surnameToDelete))
            {
                MessageBox.Show("Фамилия не может быть пустой", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int initialCount = students.Count;
            students.RemoveAll(s => s.FullName.Split()[0].Equals(surnameToDelete, StringComparison.OrdinalIgnoreCase));

            if (students.Count < initialCount)
            {
                SaveToXml();
                dataGridViewStudents.DataSource = null;
                dataGridViewStudents.DataSource = students;
                toolStripStatusLabel.Text = $"Студент(ы) с фамилией '{surnameToDelete}' удален(ы). Всего студентов: {students.Count}. Время: {DateTime.Now}";

            }
            else
            {
                MessageBox.Show("Студент с такой фамилией не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);    
            }
        }
    }
    public class Student
    {
        [Required(ErrorMessage = "ФИО не может быть пустым")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BirthDateValidation]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Выберите специальность")]
        public string Specialization { get; set; }

        [Range(1, 4, ErrorMessage = "Курс должен быть от 1 до 4")]
        public int Course { get; set; }

        [Range(1, 10, ErrorMessage = "Группа должна быть в пределах от 1 до 10")]
        public int Group { get; set; }

        [ValidAverageMark]
        public int AverageMark { get; set; }

        [RegularExpression("M|Ж", ErrorMessage = "Пол должен быть 'M' или 'Ж'")]
        public string Gender { get; set; }

        public WorkPlace WorkPlace { get; set; }

        public string WorkPlaceCompany => WorkPlace?.Company;
        public string WorkPlacePosition => WorkPlace?.Position;
        public int? WorkPlaceExperience => WorkPlace?.Experience;
    }


    public class WorkPlace
    {
        [Required(ErrorMessage = "Название компании не может быть пустым")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Должность не может быть пустой")]
        public string Position { get; set; }

        [Range(0, 50, ErrorMessage = "Опыт работы должен быть в пределах от 0 до 50 лет")]
        public int Experience { get; set; }
    }

    public class BirthDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date >= DateTime.Now)
                {
                    return new ValidationResult("Дата рождения не может быть в будущем.");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class ValidAverageMark : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int mark)
            {
                if (mark < 1 || mark > 10)
                {
                    return new ValidationResult("Средний балл должен быть в пределах от 1 до 10.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
