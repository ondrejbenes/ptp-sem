using Sem_Benes.API;
using System;
using System.ComponentModel;
using System.Windows;

namespace Sem_Benes
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private bool _loggedIn;

        private IUserService _userService;

        private readonly MainWindow _parentWindow;

        public Login(MainWindow parentWindow)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _parentWindow = parentWindow;
            _userService = new UserServiceImpl(UserMemoryDao.Get());
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (TxbUserName.Text == String.Empty && PsbPassword.Password == String.Empty)
            {
                LblError.Content = "Zadejte uživatelské jméno a heslo";
                return;
            }
            if (TxbUserName.Text == String.Empty)
            {
                LblError.Content = "Zadejte uživatelské jméno";
                return;
            }
            if (PsbPassword.Password == String.Empty)
            {
                LblError.Content = "Zadejte heslo";
                return;
            }
            var user = _userService.FindUserByUsername(TxbUserName.Text);
            if (user != null)
            {
                if (PasswordHash.PasswordHash.ValidatePassword(PsbPassword.Password, user.Password))
                {
                    _parentWindow.Visibility = Visibility.Visible;
                    _loggedIn = true;
                    LoginManager.LoggedInUser = user;
                    _parentWindow.ShowContent();
                    Close();
                }
                else
                    LblError.Content = "Špatné heslo";
            }
            else
                LblError.Content = "Neznámý uživatel";
        }

        private void Login_OnClosing(object sender, CancelEventArgs e)
        {
            if(!_loggedIn)
                Application.Current.Shutdown();
        }
    }
}
