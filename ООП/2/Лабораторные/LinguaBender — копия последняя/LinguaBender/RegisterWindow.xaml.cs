using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using LinguaBender.Data;
using LinguaBender.Models;
using LinguaBender.Services;
using System.IO;

namespace LinguaBender
{
    public partial class RegisterWindow : Window
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        private byte[]? _photoData;

        public RegisterWindow(IUserService userService, AppDbContext context)
        {
            InitializeComponent();
            _userService = userService;
            _context = context;

            LoadRoles();
        }

        private void LoadRoles()
        {
            var roles = _context.Roles.ToList();
            RoleComboBox.ItemsSource = roles;
            RoleComboBox.SelectedIndex = 0;
        }

        private void ChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() == true)
            {
                _photoData = File.ReadAllBytes(dialog.FileName);
                UserPhotoPreview.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (RoleComboBox.SelectedItem is not Role selectedRole)
            {
                MessageBox.Show("Выберите роль");
                return;
            }

            bool result = await _userService.RegisterAsync(
                UsernameBox.Text,
                PasswordBox.Password,
                selectedRole.RoleId,
                _photoData
            );

            if (result)
            {
                MessageBox.Show("Регистрация успешна!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже существует.");
            }
        }
    }
}
