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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestaoDeClientes.UI.Views
{
    /// <summary>
    /// Interação lógica para ClienteView.xam
    /// </summary>
    public partial class ClienteView : UserControl
    {

        CadastrarClienteView cadastrarClienteView = new CadastrarClienteView();
        ClienteRepository clienteRepository = new ClienteRepository();
        public ClienteView()
        {
            InitializeComponent();            
        }

        public static readonly RoutedEvent ConsultarDigitalizacaoButtonClickEvent = EventManager.RegisterRoutedEvent(
        "ClientesButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClienteView));

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            busySalvarCarteirasIndicator.IsBusy = false;           
        }        

        private async void btnBuscarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetAllAsync();
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.Message,"Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
        }
        private async void btnBuscarClientePorNome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetByNomeAsync("NomeTeste");
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
        }

        private async void btnCadastrarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                primeiraGrid.Children.Add(cadastrarClienteView);
                cadastrarClienteView.OnCancelarClicado += CadastrarClienteView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;       
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
            
        }

        private void CadastrarClienteView_OnCancelarClicado(object sender, EventArgs e)
        {
            gridPrincipal.IsEnabled = true;
        }

        private async void btnAtualizarClientes_Click(object sender, RoutedEventArgs e)
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

                await Update(cliente);
            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
        }

        private async void btnDeletarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = GetByNomeAsync("NomeTeste");
                await DeleteAsync(client.Id.ToString());

            }
            catch (Exception ex)
            {
                ErrorMessageBox.Show(ex.Message, "Erro", ErrorMessageBox.MessageBoxStatus.Error);
            }
        }

        private async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            try
            {
                return await clienteRepository.GetAllAsync(); 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Cliente> GetByNomeAsync(string nome)
        {
            try
            {
                return await clienteRepository.GetByNomeAsync(nome);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Cliente> GetByIdAsync(Guid id)
        {
            try
            {
                return await clienteRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task DeleteAsync(string id)
        {
            try
            {
                await clienteRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task Update(Cliente cliente)
        {
            try
            {
                await clienteRepository.UpdateAsync(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
