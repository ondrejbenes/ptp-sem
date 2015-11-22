using System;
using System.Windows;

namespace Sem_Benes
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            lbl_InputDescription.Content = question;
            txt_Input.Text = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txt_Input.SelectAll();
            txt_Input.Focus();
        }

        public string Answer
        {
            get { return txt_Input.Text; }
        }

    }
}
