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
using static GestaoDeClientes.UI.Popup.ErrorMessageBox;

namespace GestaoDeClientes.UI.Popup
{
    /// <summary>
    /// Lógica interna para ErrorMessageBoxDefault.xaml
    /// </summary>
    public partial class ErrorMessageBoxDefault : Window
    {
        public ErrorMessageBoxDefault(string mensagem)
        {
            InitializeComponent();
            txtMensagem.Text = mensagem;
        }

        public ErrorMessageBoxDefault(string titulo, string mensagem)
        {
            InitializeComponent();

            txtTitulo.Text = titulo.ToUpper();
            txtMensagem.Text = mensagem;
        }

        public ErrorMessageBoxDefault(string mensagem, MessageBoxStatus status)
        {
            InitializeComponent();

            txtMensagem.Text = mensagem;

            UpdateIconColor(status);
        }

        public ErrorMessageBoxDefault(string titulo, string mensagem, MessageBoxStatus status)
        {
            InitializeComponent();

            txtTitulo.Text = titulo.ToUpper();
            txtMensagem.Text = mensagem;

            UpdateIconColor(status);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


        private void UpdateIconColor(MessageBoxStatus status)
        {
            switch (status)
            {
                case MessageBoxStatus.Ok:
                    IcoStatus.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckCircle;
                    break;

                case MessageBoxStatus.Warning:
                    IcoStatus.Kind = MaterialDesignThemes.Wpf.PackIconKind.Warning;
                    break;

                case MessageBoxStatus.Error:
                    IcoStatus.Kind = MaterialDesignThemes.Wpf.PackIconKind.CancelCircle;
                    break;

                case MessageBoxStatus.Information:
                    IcoStatus.Kind = MaterialDesignThemes.Wpf.PackIconKind.WarningCircle;
                    break;

                default:
                    break;
            }
        }
    }
}
