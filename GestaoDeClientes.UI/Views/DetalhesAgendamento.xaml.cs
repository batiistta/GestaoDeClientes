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
    /// Interação lógica para DetalhesAgendamento.xam
    /// </summary>
    public partial class DetalhesAgendamento : UserControl, IRemoverJanela
    {
        public event EventHandler OnCancelarClicado;
        private Agendamento _agendamento;
        ClienteRepository clienteRepository = new ClienteRepository();
        ServicoRepository ServicoRepository = new ServicoRepository();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();

        public DetalhesAgendamento()
        {
            InitializeComponent();
        }
        public DetalhesAgendamento(Agendamento agendamento)
        {
            InitializeComponent();

            CarregarClientes();
            CarregarServicos();

            this.DataContext = agendamento;
            _agendamento = agendamento;

            txtDataAgendamento.Text = agendamento.DataAgendamento.ToString();
            cmbClientes.Text = agendamento.NomeCliente;
            cmbServicos.Text = agendamento.NomeServico;
        }
        private void CarregarClientes()
        {
            var clientes = clienteRepository.GetAllAsync();
            cmbClientes.ItemsSource = clientes.Result;
        }

        private void CarregarServicos()
        {
            var Servicos = ServicoRepository.GetAllAsync();
            cmbServicos.ItemsSource = Servicos.Result;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Agendamento agendamento = new Agendamento();
                agendamento.Id = _agendamento.Id;
                agendamento.NomeServico = (cmbServicos.SelectedItem as Servico).Nome;
                agendamento.IdServico = (cmbServicos.SelectedItem as Servico).Id;
                agendamento.NomeCliente = (cmbClientes.SelectedItem as Cliente).Nome;
                agendamento.IdCliente = (cmbClientes.SelectedItem as Cliente).Id;
                agendamento.DataAgendamento = txtDataAgendamento.DisplayDate;

                await agendamentoRepository.UpdateAsync(agendamento);

                GCMessageBox.Show("Agendamento atualizado com sucesso!", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, GCMessageBox.MessageBoxStatus.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }

        public void RemoverJanela()
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
    }
}
