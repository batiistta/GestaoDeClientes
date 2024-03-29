﻿using GestaoDeClientes.Domain;
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
using System.Windows.Shapes;

namespace GestaoDeClientes.UI.Views
{
    /// <summary>
    /// Lógica interna para CadastrarClienteView.xaml
    /// </summary>
    public partial class CadastrarClienteView : UserControl, IRemoverJanela
    {
        #region Propriedades
        ClienteRepository clienteRepository = new ClienteRepository();
        public event EventHandler ChildWindowClosed;        
        public event EventHandler OnCancelarClicado;
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();
        #endregion

        #region Construtor
        public CadastrarClienteView()
        {
            InitializeComponent();
            this.DataContext = new Cliente();
            txtDataNascimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }        
        #endregion

        #region Botões
        private async void btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            var dataNascimento = txtDataNascimento.SelectedDate;
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = Guid.NewGuid().ToString();
                cliente.Nome = txtNome.Text.ToUpper();
                cliente.Telefone = txtTelefone.Text;
                cliente.DataNascimento = (DateTime)dataNascimento;
                cliente.DataCadastro = DateTime.Now;
                cliente.Endereco = txtEndereco.Text;
                cliente.Ativo = true;

                await clienteRepository.AddAsync(cliente);

                GCMessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Limpar();
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtNome.Focus();
            Limpar();
            btnCadastrar.IsEnabled = false;
        }
        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtTelefone_TextChanged(object sender, TextChangedEventArgs e)
        {            
            FormatarTelefone(sender, e);
            VerificarErrosEBloquearBotao();

        }
        private void txtEndereco_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtNome_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextOnly(e.Text))
            {
                e.Handled = true;
            }
        }        
        private void txtTelefone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumberOnly(e.Text))
            {
                e.Handled = true;
            }
        }
        private void txtEndereco_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z0-9\s\-,()]*$"))
            {
                e.Handled = true;
            }
        }    
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
            ResetBindingExpressions(txtNome, txtTelefone, txtEndereco);
            txtNome.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtDataNascimento.Text = string.Empty;
        }
        public void RemoverJanela()
        {
            Limpar();
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        private void FormatarTelefone(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string digitsOnly = new string(textBox.Text.Where(char.IsDigit).ToArray());

                StringBuilder formatted = new StringBuilder();

                int digitCount = 0;
                foreach (char digit in digitsOnly)
                {
                    if (digitCount == 0)
                        formatted.Append(" (");
                    if (digitCount == 2)
                        formatted.Append(") ");
                    if (digitCount == 7)
                        formatted.Append("-");

                    formatted.Append(digit);

                    digitCount++;
                }
                textBox.Text = formatted.ToString();
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
        private void VerificarErrosEBloquearBotao()
        {
           bindingExpressions.Add(txtNome.GetBindingExpression(TextBox.TextProperty));
           bindingExpressions.Add(txtTelefone.GetBindingExpression(TextBox.TextProperty));
           bindingExpressions.Add(txtEndereco.GetBindingExpression(TextBox.TextProperty));
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
