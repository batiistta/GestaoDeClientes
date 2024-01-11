using GestaoDeClientes.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestaoDeClientes.UI.Views
{
    /// <summary>
    /// Interação lógica para DetalhesUsuarioView.xam
    /// </summary>
    public partial class DetalhesUsuarioView : UserControl
    {
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        public DetalhesUsuarioView()
        {
            InitializeComponent();
        }

        public DetalhesUsuarioView(Usuario usuario)
        {
            InitializeComponent();
            this.DataContext = usuario;
            txtNome.Text = usuario.Nome;
            txtLogin.Text = usuario.Login;
            txtEmail.Text = usuario.Email;
            txtSenha.Text = usuario.Senha;

        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
    }
}
