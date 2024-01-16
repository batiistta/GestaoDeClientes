using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.Repositories;
using GestaoDeClientes.UI.Popup;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class CadastrarProdutoView : UserControl, IRemoverJanela
    {
        #region Propriedades
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        ProdutoRepository produtoRepository = new ProdutoRepository();
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();
        #endregion

        #region Construtores
        public CadastrarProdutoView()
        {                       
            InitializeComponent();
            this.DataContext = new Produto();
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtNomeProduto.Focus();
            Limpar();
            btnCadastrar.IsEnabled = false;
        }
        private void txtNomeProduto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextOnly(e.Text))
            {
                e.Handled = true;
            }
        }
        private void txtValor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumberOnly(e.Text))
            {
                e.Handled = true;
            }
        }
        private void txtValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (decimal.TryParse(textBox.Text, out decimal value))
            {
                textBox.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N2}", value);
            }
        }
        private void txtDescricaoProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtNomeProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        #region Botões
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Limpar();
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

                GCMessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {                
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        #endregion

        #region Métodos
        private bool IsNumberOnly(string text)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9]+$");
        }
        private bool IsTextOnly(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }
        private void Limpar()
        {
            ResetBindingExpressions(txtNomeProduto, txtDescricaoProduto, txtValorCompraProduto, txtValorUnitarioProduto, txtQuantidadeProduto);
            txtNomeProduto.Text = string.Empty;
            txtDescricaoProduto.Text = string.Empty;
            txtValorCompraProduto.Text = string.Empty;
            txtValorUnitarioProduto.Text = string.Empty;
            txtQuantidadeProduto.Text = string.Empty;

            
        }
        public void RemoverJanela()
        {
            Limpar();
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
            btnCadastrar.IsEnabled = !algumErro;
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
