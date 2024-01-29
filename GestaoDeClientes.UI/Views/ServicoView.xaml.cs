using GestaoDeClientes.Domain;
using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.Repositories;
using GestaoDeClientes.UI.Popup;
using MaterialDesignThemes.Wpf;
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
    /// Interação lógica para ServicoView.xam
    /// </summary>
    public partial class ServicoView : UserControl
    {
        #region Propriedades
        DetalhesServicoView atualizarServicoView = new DetalhesServicoView();
        CadastrarServicoView cadastrarServicoView = new CadastrarServicoView();
        ServicoRepository ServicoRepository = new ServicoRepository();
        List<Servico> Servicos = new List<Servico>();
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        #endregion

        #region Construtores
        public ServicoView()
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
        private void listViewFiles_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarServicos();
            }
        }
        private void CadastrarServicoView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(cadastrarServicoView);
            gridPrincipal.IsEnabled = true;
            CarregarServicos();
        }
        private void DetalhesServicoView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(atualizarServicoView);
            gridPrincipal.IsEnabled = true;
            CarregarServicos();
        }
        #region Botões
        private void btnCadastrarServico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                primeiraGrid.Children.Add(cadastrarServicoView);
                cadastrarServicoView.OnCancelarClicado += CadastrarServicoView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void btnBuscarServicos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    listServicos.ItemsSource = await GetAllAsync();
                    return;
                }
                Servicos = (await GetAllAsync()).ToList();
                listServicos.ItemsSource = Servicos.Where(c => c.Nome.ToLower().Contains(txtSearch.Text.ToLower()));
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }
        private async void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnDeletar = sender as Button;

                Servico ServicoParaDeletar = btnDeletar.DataContext as Servico;

                bool confirmacao = GCMessageBox.Confirm("Deseja realmente deletar o Servico?", "Deletar", GCMessageBox.MessageBoxStatus.Warning);

                if (confirmacao)
                {
                    ServicoRepository ServicoRepository = new ServicoRepository();
                    await ServicoRepository.DeleteAsync(ServicoParaDeletar.Id);
                    MessageBox.Show("Servico deletado com sucesso!");
                    CarregarServicos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnAtualizar = sender as Button;

                Servico ServicoParaAtualizar = btnAtualizar.DataContext as Servico;

                atualizarServicoView = new DetalhesServicoView(ServicoParaAtualizar);

                primeiraGrid.Children.Add(atualizarServicoView);
                atualizarServicoView.OnCancelarClicado += DetalhesServicoView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            listServicos.ItemsSource = null;
            this.Visibility = Visibility.Hidden;
        }
        #endregion
        #endregion

        #region Métodos
        private void RemoverJanelasFilhas()
        {
            foreach (var child in primeiraGrid.Children.OfType<IRemoverJanela>().ToList())
            {
                child.RemoverJanela();
            }

            gridPrincipal.IsEnabled = true;
        }
        private void CarregarServicos()
        {
            try
            {
                Servicos = ServicoRepository.GetAllAsync().Result.ToList();
                listServicos.ItemsSource = Servicos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<IEnumerable<Servico>> GetAllAsync()
        {
            try
            {
                return await ServicoRepository.GetAllAsync();
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
                CarregarServicos();
            }
        }
    }
}
