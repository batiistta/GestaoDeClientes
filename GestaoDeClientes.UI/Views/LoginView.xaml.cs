using GestaoDeClientes.Infra;
using GestaoDeClientes.UI.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace GestaoDeClientes.UI.Views
{
    /// <summary>
    /// Lógica interna para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public bool isAutenticado { get; set; }
        public LoginView()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            //busySalvarCarteirasIndicator.IsBusy = true;
            //var userName = txtUserName.Text;
            //var password = pbPassword.Password;
            //try
            //{
            //    if (String.IsNullOrEmpty(txtUserName.Text))
            //    {
            //        ErrorMessageBox.Show("O preenchimento do campo Username é obrigatório!", "Error", ErrorMessageBox.MessageBoxStatus.Error);
            //        return;
            //    }

            //    if (String.IsNullOrEmpty(pbPassword.Password))
            //    {
            //        ErrorMessageBox.Show("O preenchimento do campo Senha é obrigatório!", "Error", ErrorMessageBox.MessageBoxStatus.Error);
            //        return;
            //    }
            //    this.isAutenticado = true;
            //    busySalvarCarteirasIndicator.IsBusy = false;
            //    this.Hide();
            //}
            //catch(Exception ex)
            //{
            //    busySalvarCarteirasIndicator.IsBusy = false;
            //    ErrorMessageBox.Show(ex.Message, "Error", ErrorMessageBox.MessageBoxStatus.Error);
            //    txtUserName.Text = string.Empty;
            //    pbPassword.Password = string.Empty;
            //    txtUserName.Focus();
            //}

            StartupRepository startupRepository = new StartupRepository();
            await startupRepository.VerifyDatabase();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();
        }

    }
}
