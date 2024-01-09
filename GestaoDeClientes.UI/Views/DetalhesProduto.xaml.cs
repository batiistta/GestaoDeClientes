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
    /// Interação lógica para DetalhesProdutoView.xam
    /// </summary>
    public partial class DetalhesProdutoView : UserControl
    {
        private Produto _produto;
        private ProdutoView _produtoView;
        public DetalhesProdutoView()
        {
            InitializeComponent();
        }

        public DetalhesProdutoView(Produto produto, ProdutoView produtoView)
        {
            InitializeComponent();

            _produto = produto;
            _produtoView = produtoView;

            txtNomeProduto.Text = _produto.Nome;
            txtDescricaoProduto.Text = _produto.Descricao;
            txtValorCompraProduto.Text = _produto.ValorCompra.ToString();
            txtValorUnitarioProduto.Text = _produto.ValorUnitario.ToString();
            txtQuantidadeProduto.Text = _produto.Quantidade.ToString();

        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomeProduto.Text.Any())
            {
                _produto.Nome = txtNomeProduto.Text;
            }

            if (produtoAtivo.IsChecked == false)
            {
                _produto.Ativo = false;
            }

            ProdutoRepository produtoRepository = new ProdutoRepository();
            produtoRepository.UpdateAsync(_produto);

            MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            ProdutoView produtoView = new ProdutoView();
            this.Content = produtoView;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
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
    }
}
