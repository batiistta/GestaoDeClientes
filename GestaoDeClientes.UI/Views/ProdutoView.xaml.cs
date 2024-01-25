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
    /// Interação lógica para ProdutoView.xam
    /// </summary>
    public partial class ProdutoView : UserControl
    {
        #region Propriedades
        DetalhesProdutoView atualizarProdutoView = new DetalhesProdutoView();
        CadastrarProdutoView cadastrarProdutoView = new CadastrarProdutoView();
        ProdutoRepository produtoRepository = new ProdutoRepository();
        List<Produto> produtos = new List<Produto>();
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        #endregion

        #region Construtores
        public ProdutoView()
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
                CarregarProdutos();
            }
        }
        private void CadastrarProdutoView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(cadastrarProdutoView);
            gridPrincipal.IsEnabled = true;
            CarregarProdutos();
        }
        private void DetalhesProdutoView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(atualizarProdutoView);
            gridPrincipal.IsEnabled = true;
            CarregarProdutos();
        }
        #region Botões
        private void btnCadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                primeiraGrid.Children.Add(cadastrarProdutoView);
                cadastrarProdutoView.OnCancelarClicado += CadastrarProdutoView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void btnBuscarProdutos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    listProdutos.ItemsSource = await GetAllAsync();
                    return;
                }
                produtos = (await GetAllAsync()).ToList();
                listProdutos.ItemsSource = produtos.Where(c => c.Nome.ToLower().Contains(txtSearch.Text.ToLower()));
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

                Produto produtoParaDeletar = btnDeletar.DataContext as Produto;

                bool confirmacao = GCMessageBox.Confirm("Deseja realmente deletar o produto?", "Deletar", GCMessageBox.MessageBoxStatus.Warning);

                if (confirmacao)
                {
                    ProdutoRepository produtoRepository = new ProdutoRepository();
                    await produtoRepository.DeleteAsync(produtoParaDeletar.Id);
                    MessageBox.Show("Produto deletado com sucesso!");
                    CarregarProdutos();
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

                Produto produtoParaAtualizar = btnAtualizar.DataContext as Produto;

                atualizarProdutoView = new DetalhesProdutoView(produtoParaAtualizar);

                primeiraGrid.Children.Add(atualizarProdutoView);
                atualizarProdutoView.OnCancelarClicado += DetalhesProdutoView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            listProdutos.ItemsSource = null;
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
        private void CarregarProdutos()
        {
            try
            {
                produtos = produtoRepository.GetAllAsync().Result.ToList();
                listProdutos.ItemsSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<IEnumerable<Produto>> GetAllAsync()
        {
            try
            {
                return await produtoRepository.GetAllAsync();
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
                CarregarProdutos();
            }
        }
    }
}
