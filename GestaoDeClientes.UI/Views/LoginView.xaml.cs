using GestaoDeClientes.Infra;
using GestaoDeClientes.Infra.Repositories;
using GestaoDeClientes.Shared;
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
            var userName = txtUserName.Text;
            var password = pbPassword.Password;
            try
            {
                if (String.IsNullOrEmpty(txtUserName.Text))
                {
                    GCMessageBox.Show("O preenchimento do campo Username é obrigatório!", "Error", GCMessageBox.MessageBoxStatus.Error);
                    return;
                }

                if (String.IsNullOrEmpty(pbPassword.Password))
                {
                    GCMessageBox.Show("O preenchimento do campo Senha é obrigatório!", "Error", GCMessageBox.MessageBoxStatus.Error);
                    return;
                }

                busySalvarCarteirasIndicator.IsBusy = true;
                UsuarioRepository usuarioRepository = new UsuarioRepository();

                this.isAutenticado = usuarioRepository.ValidarUsuario(userName, password);

                if (!this.isAutenticado)
                {
                    busySalvarCarteirasIndicator.IsBusy = false;
                    GCMessageBox.Show("Usuário ou senha inválidos!", "Error", GCMessageBox.MessageBoxStatus.Error);
                    txtUserName.Text = string.Empty;
                    pbPassword.Password = string.Empty;
                    txtUserName.Focus();
                    return;
                }

                Global.Instance.UsuarioAutenticado = usuarioRepository.GetByUsername(userName);

                busySalvarCarteirasIndicator.IsBusy = false;
                this.Hide();
            }
            catch (Exception ex)
            {
                busySalvarCarteirasIndicator.IsBusy = false;
                GCMessageBox.Show(ex.Message, "Error", GCMessageBox.MessageBoxStatus.Error);
                txtUserName.Text = string.Empty;
                pbPassword.Password = string.Empty;
                txtUserName.Focus();
            }



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
