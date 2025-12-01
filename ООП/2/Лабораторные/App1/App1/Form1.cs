using System.Diagnostics;

namespace App1
{
    public interface IConverter
    {
        void Convert(double value, string conversionType);
    }
    public partial class Form1 : Form
    {
        public class Calculator : IConverter 
        {
            public delegate void ConversionHandler(double result);
            public event ConversionHandler OnConversion;
            public void Convert(double value, string conversionType)
            {
                double result = conversionType switch
                {
                    "Метры в Футы" => value * 3.28084,
                    "Футы в Метры" => value / 3.28084,
                    "Килограммы в Фунты" => value * 2.20462,
                    "Фунты в Килограммы" => value / 2.20462,
                    "Литры в Галлоны" => value * 0.264172,
                    "Галлоны в Литры" => value / 0.264172,
                    _ => throw new ArgumentException("Неподдерживаемый тип конверсии")
                };
                OnConversion?.Invoke(result);
            }
        }

        private readonly IConverter _calculator;
        public Form1()
        {
            InitializeComponent();
            _calculator = new Calculator();
            ((Calculator)_calculator).OnConversion += DisplayResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double inputValue = double.Parse(textBox1.Text);
                string conversionType = comboBox1.SelectedItem.ToString();
                _calculator.Convert(inputValue, conversionType);
            }
            catch (Exception ex)
            {
                label4.Visible = true;
                label4.Text = $"Ошибка:{ex.Message}";
                label4.ForeColor = Color.Red;
            }
        }

        private void DisplayResult(double result)
        {
            textBox2.Text = result.ToString("F2");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
