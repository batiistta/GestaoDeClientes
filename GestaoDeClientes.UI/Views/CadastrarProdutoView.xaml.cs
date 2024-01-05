using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interação lógica para CadastrarProdutoView.xam
    /// </summary>
    public partial class CadastrarProdutoView : UserControl
    {
        private ProdutoView _produtoView;
        public CadastrarProdutoView(ProdutoView produtoview)
        {
            InitializeComponent();
            _produtoView = produtoview;
        }
        public CadastrarProdutoView()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProdutoView produtoView = new ProdutoView();
                this.Content = produtoView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            produto.Nome = txtNomeProduto.Text;
            produto.Descricao = txtDescricaoProduto.Text;
            produto.ValorCompra = Convert.ToDecimal(txtValorCompraProduto.Text);
            produto.ValorUnitario = Convert.ToDecimal(txtValorUnitarioProduto.Text);
            produto.Quantidade = Convert.ToInt32(txtQuantidadeProduto.Text);
            produto.Ativo = true;

            try
            {
                ProdutoRepository produtoRepository = new ProdutoRepository();
                await produtoRepository.AddAsync(produto);
                MessageBox.Show("Produto cadastrado com sucesso!");
                ProdutoView produtoView = new ProdutoView();
                this.Content = produtoView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
