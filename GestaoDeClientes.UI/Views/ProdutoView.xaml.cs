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
    }
}
