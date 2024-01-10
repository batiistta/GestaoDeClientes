using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Repositories;
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
        List<Produto> produtos = new List<Produto>();

        public ProdutoView()
        {
            InitializeComponent();
        }

        private void listViewFiles_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarProdutos();
            }
        }

        private void CarregarProdutos()
        {
            try
            {
                ProdutoRepository produtoRepository = new ProdutoRepository();
                produtos = produtoRepository.GetAllAsync().Result.ToList();
                listProdutos.ItemsSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CadastrarProdutoView cadastrarProdutoView = new CadastrarProdutoView();
                this.Content = cadastrarProdutoView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnDeletar = sender as Button;

                Produto produtoParaDeletar = btnDeletar.DataContext as Produto;

                if (MessageBox.Show("Deseja realmente deletar o produto " + produtoParaDeletar.Nome + "?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ProdutoRepository produtoRepository = new ProdutoRepository();
                    produtoRepository.DeleteAsync(produtoParaDeletar.Id);
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

                DetalhesProdutoView atualizarProdutoView = new DetalhesProdutoView(produtoParaAtualizar, this);

                this.Content = atualizarProdutoView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsVisible)
            {
                if (!(this.Content is ProdutoView))
                {
                    ProdutoView produtoView = new ProdutoView();
                    this.Content = produtoView;
                }
            }
        }
    }
}
