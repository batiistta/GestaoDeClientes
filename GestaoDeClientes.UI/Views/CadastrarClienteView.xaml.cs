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
        public CadastrarClienteView()
        {
            InitializeComponent();
            this.DataContext = new Cliente();            
        }

        private async void btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{        
            //    Cliente cliente = new Cliente();
            //    cliente.Id = Guid.NewGuid();
            //    cliente.Nome = "NomeTeste";
            //    cliente.Telefone = "TelefoneTeste";
            //    cliente.DataNascimento = DateTime.Now;
            //    cliente.DataCadastro = DateTime.Now;
            //    cliente.Endereco = "EnderecoTeste";
            //    cliente.Ativo = true;

            //    await clienteRepository.AddAsync(cliente);

            //    ErrorMessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", ErrorMessageBox.MessageBoxStatus.Ok);
            //}
            //catch (Exception ex)
            //{
            //    ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            //}
        }

        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarCampos();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
            FecharUserControl();
            this.Visibility = Visibility.Hidden;
        }


        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtTelefone.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            btnCadastrar.IsEnabled = false;            
        }

        private void VerificarCampos()
        {
            BindingExpression bindingExpression = txtNome.GetBindingExpression(TextBox.TextProperty);
            bindingExpression.UpdateSource();
            if (bindingExpression?.HasError == true)
            {
                btnCadastrar.IsEnabled = false;
                // Se houver erros, mostra a mensagem de erro
                //MessageBox.Show(bindingExpression.ValidationError.ErrorContent.ToString(), "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                btnCadastrar.IsEnabled = true;
            }
        }

        private void FecharUserControl()
        {
            // Dispara o evento de fechamento
            ChildWindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LimparCampos();
        }
    }
}
