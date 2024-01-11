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
    /// Interação lógica para UsuarioView.xam
    /// </summary>
    public partial class UsuarioView : UserControl
    {
        public UsuarioView()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsVisible)
            {
                if (!(this.Content is UsuarioView))
                {
                    UsuarioView usuarioView = new UsuarioView();
                    this.Content = usuarioView;
                }
            }
        }

        private void CarregarUsuarios()
        {
            try
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                var usuarios = usuarioRepository.GetAll();
                listViewUsuarios.ItemsSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCadastrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CadastrarUsuarioView cadastrarUsuarioView = new CadastrarUsuarioView();
                this.Content = cadastrarUsuarioView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listViewUsuarios_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarUsuarios();
            }
        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
