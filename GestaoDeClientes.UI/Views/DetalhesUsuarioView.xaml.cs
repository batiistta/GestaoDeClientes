using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Repositories;
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
        public Usuario _usuario;
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        public DetalhesUsuarioView()
        {
            InitializeComponent();
        }

        public DetalhesUsuarioView(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            this.DataContext = usuario;
            txtNome.Text = usuario.Nome;
            txtLogin.Text = usuario.Login;
            txtEmail.Text = usuario.Email;
            txtSenha.Text = usuario.Senha;
            usuarioAtivo.IsChecked = usuario.Ativo;
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text.Any())
            {
                _usuario.Login = txtLogin.Text;
            }

            if (txtNome.Text.Any())
            {
                _usuario.Nome = txtNome.Text;
            }

            if (txtEmail.Text.Any())
            {
                _usuario.Email = txtEmail.Text;
            }

            if (txtSenha.Text.Any())
            {
                _usuario.Senha = txtSenha.Text;
            }

            if (usuarioAtivo.IsChecked == true)
            {
                _usuario.Ativo = true;
            }
            else
            {
                _usuario.Ativo = false;
            }

            try
            {
                usuarioRepository.Update(_usuario);
                GCMessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
    }
}
