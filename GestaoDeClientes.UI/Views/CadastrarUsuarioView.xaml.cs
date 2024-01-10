using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Repositories;
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
    /// Interação lógica para CadastrarUsuarioView.xam
    /// </summary>
    public partial class CadastrarUsuarioView : UserControl
    {
        public CadastrarUsuarioView()
        {
            InitializeComponent();
        }

        private void btnCadastrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (!txtNome.Text.Any() || !txtLogin.Text.Any() ||
                !txtSenha.Text.Any() || !txtEmail.Text.Any())
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            else
            {
                Usuario usuario = new Usuario();
                usuario.Id = Guid.NewGuid().ToString();
                usuario.Nome = txtNome.Text;
                usuario.Login = txtLogin.Text;
                usuario.Senha = txtSenha.Text;
                usuario.Email = txtEmail.Text;
                usuario.DataCadastro = DateTime.Now;

                try
                {
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    usuarioRepository.AddAsync(usuario);
                    MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    UsuarioView usuarioView = new UsuarioView();
                    this.Content = usuarioView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
