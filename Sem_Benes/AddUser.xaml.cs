using System.Windows;
using Sem_Benes.Model;

namespace Sem_Benes
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public User User { get { return parseUser(); } }

        public AddUser()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if(User != null)
                DialogResult = true;
        }

        private User parseUser()
        {
            if (TxbUserName.Text == "" || PsbPassword.Password == "" || TxbFirstName.Text == "" ||
                TxbLastName.Text == "")
            {
                MessageBox.Show("Vyplňte všechna pole", "Chyba");
                return null;
            }

            var user = new User
            {
                Id = -1,
                Username = TxbUserName.Text,
                Password = PasswordHash.PasswordHash.CreateHash(PsbPassword.Password),
                FirstName = TxbFirstName.Text,
                LastName = TxbLastName.Text,
                Role = ChbIsAdmin.IsChecked == true ? UserRole.Admin : UserRole.AppUser
            };

            return user;
        }
    }
}
