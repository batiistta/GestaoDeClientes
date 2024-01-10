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
    /// Interação lógica para DetalhesProdutoView.xam
    /// </summary>
    public partial class DetalhesProdutoView : UserControl, IRemoverJanela
    {
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        private string id;
        ProdutoRepository produtoRepository = new ProdutoRepository();
        public DetalhesProdutoView()
        {
            InitializeComponent();
        }

        public DetalhesProdutoView(Produto produto)
        {
            InitializeComponent();
            this.DataContext = produto;
            txtNomeProduto.Text = produto.Nome;
            txtDescricaoProduto.Text = produto.Descricao;
            txtValorCompraProduto.Text = produto.ValorCompra.ToString();
            txtValorUnitarioProduto.Text = produto.ValorUnitario.ToString();
            txtQuantidadeProduto.Text = produto.Quantidade.ToString();            
        }

        #region Botões
        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto produto = new Produto();
                produto.Id = id;
                produto.Nome = txtNomeProduto.Text;
                produto.Descricao = txtDescricaoProduto.Text;
                produto.ValorCompra = Convert.ToDecimal(txtValorCompraProduto.Text);
                produto.ValorUnitario = Convert.ToDecimal(txtValorUnitarioProduto.Text);
                produto.Quantidade = Convert.ToInt32(txtQuantidadeProduto.Text);
                produto.Ativo = true;

                await produtoRepository.UpdateAsync(produto);
                ErrorMessageBox.Show("Produto atualizado com sucesso!", "Sucesso", ErrorMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {                
                ErrorMessageBox.Show("Erro ao atualizar produto!", "Erro", ErrorMessageBox.MessageBoxStatus.Error);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #region Métodos
        public void RemoverJanela()
        {
            var parent = this.Parent as Panel;
            if (parent != null)
            {
                parent.Children.Remove(this);
            }
        }
        #endregion

    }
}
