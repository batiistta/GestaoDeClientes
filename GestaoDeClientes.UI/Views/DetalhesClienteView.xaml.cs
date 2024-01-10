using GestaoDeClientes.Domain;
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
    /// Interação lógica para DetalhesClienteView.xam
    /// </summary>
    public partial class DetalhesClienteView : UserControl, IRemoverJanela
    {
        #region Propriedades
        ClienteRepository clienteRepository = new ClienteRepository();
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        private string id;
        #endregion

        #region Construtores
        public DetalhesClienteView()
        {
            InitializeComponent();
        }

        public DetalhesClienteView(Cliente cliente)
        {
            InitializeComponent();
            this.DataContext = cliente;
            txtNome.Text = cliente.Nome;
            txtTelefone.Text = cliente.Telefone;
            txtDataNascimento.Text = cliente.DataNascimento.ToString("dd/MM/yyyy");
            txtEndereco.Text = cliente.Endereco;    
            id = cliente.Id;
        }
        #endregion

        #region Botões
        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = id;
                cliente.Nome = txtNome.Text;
                cliente.Telefone = txtTelefone.Text;
                cliente.DataNascimento = DateTime.Parse(txtDataNascimento.Text);
                cliente.DataCadastro = DateTime.Now;
                cliente.Endereco = txtEndereco.Text;

                await clienteRepository.UpdateAsync(cliente);
                ErrorMessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", ErrorMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
                ErrorMessageBox.Show("Erro ao atualizar cliente!", "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
            
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Eventos
        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bindingExpression = txtNome.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
            if (bindingExpression?.HasError == true)
            {
                btnAtualizar.IsEnabled = false;
            }
            else
            {
                btnAtualizar.IsEnabled = true;
            }
        }
        private void txtTelefone_TextChanged(object sender, TextChangedEventArgs e)
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
