using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Repositories;
using GestaoDeClientes.UI.Popup;
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
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        ProdutoRepository produtoRepository = new ProdutoRepository();
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
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }

        private async void btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto produto = new Produto();
                produto.Id = Guid.NewGuid().ToString();
                produto.Nome = txtNomeProduto.Text;
                produto.Descricao = txtDescricaoProduto.Text;
                produto.ValorCompra = Convert.ToDecimal(txtValorCompraProduto.Text);
                produto.ValorUnitario = Convert.ToDecimal(txtValorUnitarioProduto.Text);
                produto.Quantidade = Convert.ToInt32(txtQuantidadeProduto.Text);
                produto.Ativo = true;

                await produtoRepository.AddAsync(produto);

                ErrorMessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", ErrorMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {                
                ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
