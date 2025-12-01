
using System.Windows;
using ADO.NET.Models;
using ADO.NET.Data;


namespace ADO.NET
{
    public partial class UserDialog : Window
    {
        public User User { get; set; }
        public List<Role> Roles { get; }

        public UserDialog()
        {
            InitializeComponent();
            var roleRepo = new RoleRepository();
            Roles = roleRepo.GetAllRoles();
            User = new User();
            DataContext = this;
        }

        public UserDialog(User user) : this()
        {
            User = new User
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

}

