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
        private ProdutoView _produtoView;
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        ProdutoRepository produtoRepository = new ProdutoRepository();
        private bool hasError;
        #endregion

        #region Construtores
        public CadastrarProdutoView(ProdutoView produtoview)
        {
            InitializeComponent();
            _produtoView = produtoview;
        }
        public CadastrarProdutoView()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtNomeProduto.Focus();
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
            VerificarErrosEBloquearBotao();
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
            if (!IsTextOnly(txtNomeProduto.Text))
            {
                txtNomeProduto.Text = string.Empty;
            }
            VerificarErrosEBloquearBotao();
        }
        #region Botões
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
            txtNomeProduto.Text = string.Empty;
            txtDescricaoProduto.Text = string.Empty;
            txtValorCompraProduto.Text = string.Empty;
            txtValorUnitarioProduto.Text = string.Empty;
            txtQuantidadeProduto.Text = string.Empty;
        }
        public void RemoverJanela()
        {
            Limpar();
            var parent = this.Parent as Panel;
            if (parent != null)
            {
                parent.Children.Remove(this);
            }
        }
        private bool VerificarErrosEmCampos(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                bindingExpression?.UpdateSource();

                if (bindingExpression?.HasError == true)
                {
                    return true;
                }
            }
            return false;
        }
        private void VerificarErrosEBloquearBotao()
        {
            var textBoxesNaGrid = primeiraGrid.Children.OfType<TextBox>().ToArray();

            bool algumErro = VerificarErrosEmCampos(textBoxesNaGrid);

            btnCadastrar.IsEnabled = !algumErro;
        }
        #endregion
    }
}
