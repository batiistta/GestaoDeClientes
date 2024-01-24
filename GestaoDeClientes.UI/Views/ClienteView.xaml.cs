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
    /// Interação lógica para ClienteView.xam
    /// </summary>
    public partial class ClienteView : UserControl
    {
        #region Propriedades
        DetalhesClienteView detalhesClienteView = new DetalhesClienteView();
        CadastrarClienteView cadastrarClienteView = new CadastrarClienteView();
        ClienteRepository clienteRepository = new ClienteRepository();
        List<Cliente> clientes = new List<Cliente>();
        #endregion

        #region Construtores
        public ClienteView()
        {
            InitializeComponent();            
        }
        #endregion

        #region Eventos
        public static readonly RoutedEvent ConsultarDigitalizacaoButtonClickEvent = EventManager.RegisterRoutedEvent(
       "ClientesButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClienteView));
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!this.IsVisible)
            {
                RemoverJanelasFilhas();
            }
        }
        private void listViewFiles_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarClientes();
            }
        }
        private void CadastrarClienteView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(cadastrarClienteView);
            gridPrincipal.IsEnabled = true;
            CarregarClientes();
        }
        private void DetalhesClienteView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(detalhesClienteView);
            gridPrincipal.IsEnabled = true;
            CarregarClientes();
        }
        #endregion

        #region Botões
        private async void btnBuscarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    listClientes.ItemsSource = await GetAllAsync();
                    return;
                }
                clientes = (await GetAllAsync()).ToList();
                listClientes.ItemsSource = clientes.Where(c => c.Nome.ToLower().Contains(txtSearch.Text.ToLower()));
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }        
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                primeiraGrid.Children.Add(cadastrarClienteView);
                cadastrarClienteView.OnCancelarClicado += CadastrarClienteView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                cadastrarClienteView.OnCancelarClicado += CadastrarClienteView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }

        }
        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnAtualizar = sender as Button;

                Cliente cliente = btnAtualizar.DataContext as Cliente;

                detalhesClienteView = new DetalhesClienteView(cliente);

                primeiraGrid.Children.Add(detalhesClienteView);

                detalhesClienteView.OnCancelarClicado += DetalhesClienteView_OnCancelarClicado;

                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }

            detalhesClienteView.OnCancelarClicado += DetalhesClienteView_OnCancelarClicado;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            listClientes.ItemsSource = null;
            this.Visibility = Visibility.Hidden;
        }
        private async void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnDeletar = sender as Button;
                Cliente clienteParaDeletar = btnDeletar.DataContext as Cliente;

                if (GCMessageBox.Show("Deseja realmente deletar o produto " + clienteParaDeletar.Nome + "?", "Atenção", GCMessageBox.MessageBoxStatus.Ok))
                {
                    await DeleteAsync(clienteParaDeletar.Id.ToString());
                    GCMessageBox.Show("Produto deletado com sucesso!", GCMessageBox.MessageBoxStatus.Ok);
                    CarregarClientes();
                }
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }

        }
        #endregion

        #region Métodos

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
        private void CarregarClientes()
        {
            try
            {
                clientes = clienteRepository.GetAllAsync().Result.ToList();
                listClientes.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoverJanelasFilhas()
        {
            foreach (var child in primeiraGrid.Children.OfType<IRemoverJanela>().ToList())
            {
                child.RemoverJanela();                
            }

            gridPrincipal.IsEnabled = true;
        }
        #endregion
    }
}
