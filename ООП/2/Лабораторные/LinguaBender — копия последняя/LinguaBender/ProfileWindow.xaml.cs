using System.Windows;
using System.Windows.Controls;
using LinguaBender.Models;
using LinguaBender.Services;

namespace LinguaBender
{

    public partial class ProfileWindow : Window
    {
        private readonly IUserService _userService;
        private User _user;

        public ProfileWindow(IUserService userService, User user)
        {
            InitializeComponent();
            _userService = userService;
            _user = user;

            UsernameBox.Text = user.Name;
            PasswordBox.Password = user.Password;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            _user.Name = UsernameBox.Text;
            _user.Password = PasswordBox.Password;
            await _userService.UpdateUserAsync(_user);
            MessageBox.Show("Данные обновлены!");
        }
    }
}
