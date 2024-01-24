using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
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
    public partial class DetalhesProdutoView : UserControl, IRemoverJanela
    {
        #region Propriedades
        public event EventHandler OnCancelarClicado;
        ProdutoRepository produtoRepository = new ProdutoRepository();
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();
        private Produto _produto;

        #endregion

        #region Construtores
        public DetalhesProdutoView()
        {
            InitializeComponent();
        }

        public DetalhesProdutoView(Produto produto)
        {
            InitializeComponent();
            this.DataContext = produto;
            _produto = produto;
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtNomeProduto.Focus();
            btnAtualizar.IsEnabled = false;
        }

        private void txtText_textChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        #endregion

        #region Botões
        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomeProduto.Text.Any())
            {
                _produto.Nome = txtNomeProduto.Text;
            }

            if (produtoAtivo.IsChecked == false)
            {
                _produto.Ativo = false;
            }                        
            await produtoRepository.UpdateAsync(_produto);

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
        #endregion

        #region Métodos
        public void RemoverJanela()
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        private void VerificarErrosEBloquearBotao()
        {
            bindingExpressions.Add(txtNomeProduto.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtDescricaoProduto.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtQuantidadeProduto.GetBindingExpression(TextBox.TextProperty));
            bool algumErro = bindingExpressions.Any(x =>
            {
                x?.UpdateSource();
                return x?.HasError == true;
            });
            btnAtualizar.IsEnabled = !algumErro;
            bindingExpressions.Clear();
        }
        private void ResetBindingExpressions(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

                bindingExpression?.UpdateTarget();
            }
        }
        #endregion
    }
}
