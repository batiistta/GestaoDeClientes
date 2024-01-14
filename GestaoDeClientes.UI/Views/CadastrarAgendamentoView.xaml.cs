using GestaoDeClientes.Domain;
using GestaoDeClientes.Domain.Models;
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
    /// Interação lógica para CadastrarAgendamentoView.xam
    /// </summary>
    public partial class CadastrarAgendamentoView : UserControl
    {
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        ClienteRepository clienteRepository = new ClienteRepository();
        ProdutoRepository produtoRepository = new ProdutoRepository();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        public CadastrarAgendamentoView()
        {
            InitializeComponent();
            CarregarClientes();
            CarregarProdutos();
        }

        private void CarregarClientes()
        {
            var clientes = clienteRepository.GetAllAsync();
            cmbClientes.ItemsSource = clientes.Result;
        }

        private void CarregarProdutos()
        {
            var produtos = produtoRepository.GetAllAsync();
            cmbProdutos.ItemsSource = produtos.Result;
        }

        private async void btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Agendamento agendamento = new Agendamento();
                agendamento.IdCliente = (cmbClientes.SelectedItem as Cliente).Id;
                agendamento.IdProduto = (cmbProdutos.SelectedItem as Produto).Id;
                agendamento.DataAgendamento = txtDataAgendamento.DisplayDate;

                await agendamentoRepository.AddAsync(agendamento);
                MessageBox.Show("Agendamento cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
    }
}
