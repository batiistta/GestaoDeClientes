using GestaoDeClientes.Domain;
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
    public partial class CadastrarClienteView : UserControl
    {
        public event EventHandler ChildWindowClosed;
        ClienteRepository clienteRepository = new ClienteRepository();
        public event EventHandler OnCancelarClicado;

        public CadastrarClienteView()
        {
            InitializeComponent();
            this.DataContext = new Cliente();            
        }        

        #region Botões
        private async void btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = Guid.NewGuid();
                cliente.Nome = "NomeTeste";
                cliente.Telefone = "TelefoneTeste";
                cliente.DataNascimento = DateTime.Now;
                cliente.DataCadastro = DateTime.Now;
                cliente.Endereco = "EnderecoTeste";
                cliente.Ativo = true;

                await clienteRepository.AddAsync(cliente);

                ErrorMessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", ErrorMessageBox.MessageBoxStatus.Ok);
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
            this.Visibility = Visibility.Hidden;
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
                btnCadastrar.IsEnabled = false;
            }
            else
            {
                btnCadastrar.IsEnabled = true;
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
        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            btnCadastrar.IsEnabled = false;            
        }
        private bool IsNumberOnly(string text)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9]+$");
        }
        private bool IsTextOnly(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LimparCampos();
        }        
        #endregion
    }
}
