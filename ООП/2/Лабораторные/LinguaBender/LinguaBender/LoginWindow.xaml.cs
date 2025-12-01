using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace LinguaBender
{

    public partial class LoginWindow : Window
    {
        private Cursor _customCursor;
        private Cursor _customPointerCursor;

        private const string ADMIN_LOGIN = "admin";
        private const string ADMIN_PASSWORD = "admin";
        private const string USER_LOGIN = "user";
        private const string USER_PASSWORD = "user";
        public LoginWindow()
        {
            InitializeComponent();
            LoadCustomCursors();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string role = "";

            if (loginTextBox.Text == ADMIN_LOGIN && passwordTextBox.Password==ADMIN_PASSWORD)
            {
                role = "admin";
            }
            else if (loginTextBox.Text == USER_LOGIN && passwordTextBox.Password == USER_PASSWORD)
            {
                role= "user";   
            }
            else
            {
                MessageBox.Show("неверные данные для входа!");
                return;
            }
            MainWindow mainWindow = new MainWindow(role);
            mainWindow.Show();
            this.Close();

        }

        private void ClosePrButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(loginTextBox.Text))
            {
                LoginButton.IsEnabled = true;
            }
            
        }
        private void LoadCustomCursors()
        {
            try
            {
                string cursorPath = @"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender\cursor.cur";
                string pointerPath = @"D:\УЧЁБА\2 курс\ООП\2\Лабораторные\LinguaBender\cursorPointer.cur";

                if (File.Exists(cursorPath))
                {
                    _customCursor = new Cursor(cursorPath);
                }

                if (File.Exists(pointerPath))
                {
                    _customPointerCursor = new Cursor(pointerPath);
                }

                if (_customCursor != null)
                {
                    this.Cursor = _customCursor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсоров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_customPointerCursor != null)
            {
                this.Cursor = _customPointerCursor;
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_customCursor != null)
            {
                this.Cursor = _customCursor;
            }
        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedLanguage = selectedItem.Tag.ToString();
                ((App)Application.Current).ChangeLanguage(selectedLanguage);
            }
        }
    }
}
