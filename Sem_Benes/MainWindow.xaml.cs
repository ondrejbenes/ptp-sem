using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Sem_Benes.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Sem_Benes.API;
using Sem_Benes.Logic;
using Sem_Benes.Test;

namespace Sem_Benes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICompanyService _companyService;
        private BindingList<Company> _companies;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _companyService = new CompanyServiceImpl(CompanyMemoryDao.Get());

            // TestUtils.CreateTestUsersAndSaveToFile();
            // TestUtils.CreateTestCompaniesAndSaveToFile();
        }

        public void ShowContent()
        {
            var user = LoginManager.LoggedInUser;
            lbl_loggedUser.Content = string.Format("Přihlášen jako: {0} {1} ({2})", user.FirstName, user.LastName,
                user.Role);
            if (!user.Role.Equals(UserRole.Admin))
                mnu_Administration.Visibility = Visibility.Collapsed;

            _companies = new BindingList<Company>(new List<Company>(_companyService.FindAllCompanies()));
            _companies.AllowNew = true;

            setDataGridSource(_companies);

            // pridani pokud je filtrovano - update data source
        }

        private void setDataGridSource(BindingList<Company> source)
        {
            dgr_Companies.ItemsSource = source;
            dgr_Companies.Columns.Remove(dgr_Companies.Columns.Single(c => c.Header.Equals("Id")));
            dgr_Companies.Columns[0].Header = "IČO";
            dgr_Companies.Columns[0].Width = new DataGridLength(10, DataGridLengthUnitType.Star);
            dgr_Companies.Columns[1].Header = "DIČ";
            dgr_Companies.Columns[1].Width = new DataGridLength(10, DataGridLengthUnitType.Star);
            dgr_Companies.Columns[2].Header = "Název firmy";
            dgr_Companies.Columns[2].Width = new DataGridLength(20, DataGridLengthUnitType.Star);
            dgr_Companies.Columns[3].Header = "Obor činnosti";
            dgr_Companies.Columns[3].Width = new DataGridLength(20, DataGridLengthUnitType.Star);
            dgr_Companies.Columns[4].Header = "Fakturační adresa";
            dgr_Companies.Columns[4].Width = new DataGridLength(40, DataGridLengthUnitType.Star);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            var login = new Login(this);
            login.Show();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            CompanyMemoryDao.Get().SaveToFile();
        }

        private void Mnu_Help_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Semestrální práce do předmětu INPTP (Pokročilé techniky programování)\nAutor: Ondřej Beneš\nZS 2015",
                "O aplikaci");
        }

        private void txb_Filter_TextChanged(object sender, EventArgs e)
        {
            filter();
        }

        private void filter()
        {
            switch ((cmb_Filter.SelectedItem as ComboBoxItem).Content as string)
            {
                case "Názvu firmy":
                    filterByName();
                    break;
                case "Adrese":
                    filterByAddress();
                    break;
                case "Oboru činnosti":
                    filterByBusinessType();
                    break;
            }
        }

        private void filterByName()
        {
            try
            {
                string filter = txb_Filter.Text.Trim().Replace("'", "''");

                if (rbt_Exact.IsChecked.Value)
                    setDataGridSource(new BindingList<Company>(_companies.Where(c => c.Name.Equals(filter)).ToList()));
                if (rbt_Contains.IsChecked.Value)
                    setDataGridSource(new BindingList<Company>(_companies.Where(c => c.Name.Contains(filter)).ToList()));
                if (rbt_StartsWith.IsChecked.Value)
                    setDataGridSource(new BindingList<Company>(_companies.Where(c => c.Name.StartsWith(filter)).ToList()));
                if (rbt_EndsWith.IsChecked.Value)
                    setDataGridSource(new BindingList<Company>(_companies.Where(c => c.Name.EndsWith(filter)).ToList()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba");
            }
        }

        private void filterByAddress()
        {
            try
            {
                string filter = txb_Filter.Text.Trim().Replace("'", "''");

                if (rbt_Exact.IsChecked.Value)
                    setDataGridSource(new BindingList<Company>(_companies.Where(c => c.Address.Equals(filter)).ToList()));
                if (rbt_Contains.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.Address.Contains(filter)).ToList()));
                if (rbt_StartsWith.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.Address.StartsWith(filter)).ToList()));
                if (rbt_EndsWith.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.Address.EndsWith(filter)).ToList()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba");
            }
        }

        private void filterByBusinessType()
        {
            try
            {
                string filter = txb_Filter.Text.Trim().Replace("'", "''");

                if (rbt_Exact.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.BusinessType.Equals(filter)).ToList()));
                if (rbt_Contains.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.BusinessType.Contains(filter)).ToList()));
                if (rbt_StartsWith.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.BusinessType.StartsWith(filter)).ToList()));
                if (rbt_EndsWith.IsChecked.Value)
                    setDataGridSource(
                        new BindingList<Company>(_companies.Where(c => c.BusinessType.EndsWith(filter)).ToList()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba");
            }
        }

        private void Dgr_Companies_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var dg = sender as DataGrid;
                var dgr = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex);
                Action action = delegate
                {
                    var comp = e.Row.Item as Company;

                    if (dgr.IsNewItem) comp.Id = -1;

                    if (comp.Name == null || comp.Address == null || comp.BusinessType == null)
                    {
                        MessageBox.Show(
                            "Název, adresa a obor činnosti musí být vyplněny",
                            "Chyba",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        (dg.ItemsSource as BindingList<Company>).Remove(comp);
                    }

                    if (!IcoValidator.IsValid(comp.Ico))
                    {
                        MessageBox.Show(
                            "Zadané IČO není validní",
                            "Chyba",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        (dg.ItemsSource as BindingList<Company>).Remove(comp);
                    }
                    else
                    {
                        var compWithId = _companyService.SaveCompany(comp);

                        if (dg.ItemsSource != _companies)
                            _companies.Add(compWithId);
                    }

                };
                Dispatcher.BeginInvoke(action, System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void Dgr_Companies_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var dg = sender as DataGrid;
            var dgr = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex);
            if (e.Key != Key.Delete || dgr.IsEditing || dgr.IsNewItem) 
                return;

            var result = MessageBox.Show(
                "Opravdu chcete smazat záznam?",
                "Smazat",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                var compWithId = _companyService.RemoveCompany(dgr.Item as Company);

                if (dg.ItemsSource != _companies)
                    _companies.Remove(compWithId);
            }
            e.Handled = (result == MessageBoxResult.No);
        }

        private void Mnu_ManageUsers_OnClick(object sender, RoutedEventArgs e)
        {
            new UsersManagment().Show();
        }

        private void Mnu_Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Mnu_Save_OnClick(object sender, RoutedEventArgs e)
        {
            CompanyMemoryDao.Get().SaveToFile();
        }

        private void Mnu_ExportXml_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML soubur (*.xml)|*.xml";
            var companySerializer = new CompanySerializer();
            if (saveFileDialog.ShowDialog() == true)
                companySerializer.SerializeCompainesToXml(_companies.ToList(), saveFileDialog.FileName);
        }
    }
}