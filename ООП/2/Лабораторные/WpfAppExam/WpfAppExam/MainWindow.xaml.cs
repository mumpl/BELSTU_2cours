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

namespace WpfAppExam
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double number1 = double.Parse(num1.Text);
                double number2 = double.Parse(num2.Text);
                string operation = (combobox.SelectedItem as ComboBoxItem).Content.ToString();
                double res = 0;
                switch(operation)
                {
                    case "+":
                        res = number1 + number2;
                        break;
                    case "-":
                        res = number1 - number2;
                        break;
                    case "*":
                        res = number1 * number2;
                        break;
                    case "/":
                        res = number1 / number2;
                        break;
                }
                result.Text = $"Результат: {res}";
            }
            catch
            {
                MessageBox.Show("Ошибка ввода данных");
            }
        }

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = inputTextBox.Text;
            //подсчет символов
            int charcount = text.Length;
            charCount.Text = $"Общее число символов в строке: {charcount}";

            //подсчет слов
            int wordcount = text.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
            WordCount.Text = $"Число слов в строке: {wordcount}";

        }

        private void ComboBoxBackgroundColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxBackgroundColor.SelectedItem is ComboBoxItem bgItem)
            {
                TextBlockColorChanged.Background = bgItem.Background;
            }
            if (ComboBoxText.SelectedItem is ComboBoxItem fgItem)
            {
                TextBlockColorChanged.Foreground = fgItem.Foreground;
            }
        }
        private void TextBoxNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBoxNumbers.Text, out int numb))
            {
                ListBoxNumbers.Items.Add($"{numb} - {(numb % 2 == 0 ? "Чётное" : "Нечётное")}");
            }

        }

        private void CInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(double.TryParse(CInput.Text,out double cels))
                FOutput.Text = $"{cels * 9/5 +32} F";
        }

        private void FInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(FInput.Text, out double far))
                COutput.Text = $"{(far - 32) * 5/9} C";
        }
    }
}