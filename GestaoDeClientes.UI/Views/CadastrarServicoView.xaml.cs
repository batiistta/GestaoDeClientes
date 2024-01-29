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
    /// Interação lógica para CadastrarServicoView.xam
    /// </summary>
    public partial class CadastrarServicoView : UserControl, IRemoverJanela
    {
        #region Propriedades
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        ServicoRepository ServicoRepository = new ServicoRepository();
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();
        #endregion

        #region Construtores
        public CadastrarServicoView()
        {                       
            InitializeComponent();
            this.DataContext = new Servico();
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtNomeServico.Focus();
            Limpar();
            btnCadastrar.IsEnabled = false;
        }
        private void txtNomeServico_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void txtDescricaoServico_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtNomeServico_TextChanged(object sender, TextChangedEventArgs e)
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
                Servico Servico = new Servico();
                Servico.Id = Guid.NewGuid().ToString();
                Servico.Nome = txtNomeServico.Text.ToUpper();
                Servico.Descricao = txtDescricaoServico.Text;
                Servico.Valor = Convert.ToDecimal(txtValorCompraServico.Text);
                Servico.Ativo = true;

                await ServicoRepository.AddAsync(Servico);

                GCMessageBox.Show("Servico cadastrado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
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
            return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9,.]+$");
        }
        private bool IsTextOnly(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }
        private void Limpar()
        {
            ResetBindingExpressions(txtNomeServico, txtDescricaoServico, txtValorCompraServico);
            txtNomeServico.Text = string.Empty;
            txtDescricaoServico.Text = string.Empty;
            txtValorCompraServico.Text = string.Empty;

            
        }
        public void RemoverJanela()
        {
            Limpar();
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        private void VerificarErrosEBloquearBotao()
        {
            bindingExpressions.Add(txtNomeServico.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtDescricaoServico.GetBindingExpression(TextBox.TextProperty));
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
