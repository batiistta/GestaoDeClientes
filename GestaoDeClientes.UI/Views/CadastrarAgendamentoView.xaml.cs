using GestaoDeClientes.Domain;
using GestaoDeClientes.Domain.Models;
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
    /// Interação lógica para CadastrarAgendamentoView.xam
    /// </summary>
    public partial class CadastrarAgendamentoView : UserControl, IRemoverJanela
    {
        #region Propriedades
        public event EventHandler OnCancelarClicado;
        ClienteRepository clienteRepository = new ClienteRepository();
        ProdutoRepository produtoRepository = new ProdutoRepository();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        #endregion

        #region Construtor
        public CadastrarAgendamentoView()
        {
            InitializeComponent();
            this.DataContext = new Agendamento();
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarClientes();
                CarregarProdutos();
            }
        }
        #endregion

        #region Botões

        private async void btnCadastar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Agendamento agendamento = new Agendamento();
                agendamento.Id = Guid.NewGuid().ToString();
                agendamento.DataAgendamento = txtDataAgendamento.DisplayDate;
                agendamento.IdCliente = (cmbClientes.SelectedItem as Cliente).Id;
                agendamento.NomeCliente = (cmbClientes.SelectedItem as Cliente).Nome;

                if (cmbProdutos.SelectedItem != null)
                {
                    agendamento.NomeProduto = (cmbProdutos.SelectedItem as Produto).Nome;
                    agendamento.IdProduto = (cmbProdutos.SelectedItem as Produto).Id;
                }
                else
                {
                    agendamento.NomeProduto = null;
                    agendamento.IdProduto = null;
                }

                await agendamentoRepository.AddAsync(agendamento);

                GCMessageBox.Show("Agendamento cadastrado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cmbClientes.SelectedItem = null;
            cmbProdutos.SelectedItem = null;
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Métodos
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
        public void RemoverJanela()
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
