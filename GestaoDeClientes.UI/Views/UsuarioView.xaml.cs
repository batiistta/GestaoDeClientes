using GestaoDeClientes.Domain;
using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
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
    /// Interação lógica para UsuarioView.xam
    /// </summary>
    public partial class UsuarioView : UserControl
    {
        #region Propriedades
        public event EventHandler OnCancelarClicado;
        CadastrarUsuarioView cadastrarUsuarioView = new CadastrarUsuarioView();
        DetalhesUsuarioView detalhesUsuarioView = new DetalhesUsuarioView();
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        List<Usuario> usuarios = new List<Usuario>();
        #endregion

        #region Construtores
        public UsuarioView()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsVisible)
            {
                RemoverJanelasFilhas();
            }
        }
        private void listViewUsuarios_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarUsuarios();
            }
        }
        private void CadastrarUsuarioView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(cadastrarUsuarioView);
            gridPrincipal.IsEnabled = true;
            CarregarUsuarios();
        }
        private void DetalhesUsuarioView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(detalhesUsuarioView);
            gridPrincipal.IsEnabled = true;
            CarregarUsuarios();
        }
        #endregion

        #region Botões
        private void btnCadastrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                primeiraGrid.Children.Add(cadastrarUsuarioView);
                cadastrarUsuarioView.OnCancelarClicado += CadastrarUsuarioView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                cadastrarUsuarioView.OnCancelarClicado += CadastrarUsuarioView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }

        }
        private async void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnDeletar = sender as Button;

                Usuario usuarioParaDeletar = btnDeletar.DataContext as Usuario;

                bool confirmacao = GCMessageBox.Confirm("Deseja realmente deletar este usuário?", "Deletar", GCMessageBox.MessageBoxStatus.Warning);

                if (confirmacao)
                {
                    await usuarioRepository.DeleteAsync(usuarioParaDeletar.Id);
                    GCMessageBox.Show("Usuário deletado com sucesso!", GCMessageBox.MessageBoxStatus.Ok);
                    CarregarUsuarios();
                }
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message);
            }
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnAtualizar = sender as Button;

                Usuario usuarioParaAtualizar = btnAtualizar.DataContext as Usuario;

                detalhesUsuarioView = new DetalhesUsuarioView(usuarioParaAtualizar);
                primeiraGrid.Children.Add(detalhesUsuarioView);
                detalhesUsuarioView.OnCancelarClicado += DetalhesUsuarioView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message);
            }

            detalhesUsuarioView.OnCancelarClicado += DetalhesUsuarioView_OnCancelarClicado;
        }
        private async void btnBuscarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    listViewUsuarios.ItemsSource = await GetAllAsync();
                    return;
                }
                usuarios = (await GetAllAsync()).ToList();
                listViewUsuarios.ItemsSource = usuarios.Where(c => c.Nome.ToLower().Contains(txtSearch.Text.ToLower()));
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            listViewUsuarios.ItemsSource = null;
            this.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Métodos
        private async void CarregarUsuarios()
        {
            try
            {
                var usuarios = await usuarioRepository.GetAllAsync();
                listViewUsuarios.ItemsSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoverJanelasFilhas()
        {
            foreach (var child in primeiraGrid.Children.OfType<IRemoverJanela>().ToList())
            {
                child.RemoverJanela();
            }

            gridPrincipal.IsEnabled = true;
        }

        private async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
                return await usuarioRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                CarregarUsuarios();
            }
        }
    }
}
