using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sem_Benes.API;
using Sem_Benes.Model;

namespace Sem_Benes
{
    /// <summary>
    /// Interaction logic for UsersManagment.xaml
    /// </summary>
    public partial class UsersManagment : Window
    {
        IUserService _userService;
        private BindingList<User> _users;

        public UsersManagment()
        {
            InitializeComponent();
            _userService = new UserServiceImpl(UserMemoryDao.Get());

            _users = new BindingList<User>(new List<User>(_userService.FindAllUsers()));
            _users.AllowNew = true;
            dgr_Users.ItemsSource = _users;

        }

        private void UsersManagment_OnClosing(object sender, CancelEventArgs e)
        {
            UserMemoryDao.Get().SaveToFile();
        }

        private void Btn_ChangePassword_OnClick(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog("Nové heslo:");
            var user = ((FrameworkElement)sender).DataContext as User;
            if (inputDialog.ShowDialog() != true) return;
            if (inputDialog.Answer != "")
            {
                user.Password = PasswordHash.PasswordHash.CreateHash(inputDialog.Answer);
                MessageBox.Show(
                    "Heslo změněno",
                    "Info",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                _userService.SaveUser(user);
            }
            else
            {
                MessageBox.Show(
                    "Neplatné heslo",
                    "Chyba",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Dgr_Users_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var dg = sender as DataGrid;
            var dgr = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex);
            if (e.Key != Key.Delete || dgr.IsEditing)
                return;

            var result = MessageBox.Show(
                "Opravdu chcete smazat uživatele?",
                "Smazat",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
                _userService.RemoveUser(dgr.Item as User);
            e.Handled = (result == MessageBoxResult.No);
        }
        
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var addUserDialog = new AddUser();
            if (addUserDialog.ShowDialog() == true)
            {
                var user = addUserDialog.User;
                if (_userService.FindUserByUsername(user.Username) != null)
                    MessageBox.Show(
                        "Uživatel s tímto uživatelským jménem již existuje",
                        "Chyba",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.No);
                else
                {
                    _users.Add(_userService.SaveUser(user));
                }
            }
        }

        private void OnIsAdminChanged(object sender, RoutedEventArgs e)
        {
            var dgc = sender as DataGridCell;
            if (!dgc.IsEditing) return;
            Action action = delegate
            {
                var user = ((FrameworkElement)sender).DataContext as User;
                user.IsAdmin = (dgc.Content as CheckBox).IsChecked.Value;

                _userService.SaveUser(user);
            };
            Dispatcher.BeginInvoke(action, System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
