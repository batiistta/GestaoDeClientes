using GestaoDeClientes.Domain;
using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.Repositories;
using GestaoDeClientes.UI.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        ServicoRepository ServicoRepository = new ServicoRepository();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        AgendamentoServicoRepository agendamentoServicoRepository = new AgendamentoServicoRepository();
        public List<string> ServicosSelecionadosIds { get; set; } = new List<string>();
        public ObservableCollection<Servico> servicosAdicionados = new ObservableCollection<Servico>();
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
                CarregarServicos();
            }
            else
            {
                servicosAdicionados.Clear();
                listviewSerivosAdicionados.Visibility = Visibility.Collapsed;
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
                agendamento.NomeCliente = (cmbClientes.SelectedItem as Cliente).Nome.ToUpper();

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
            cmbServicos.SelectedItem = null;
            ServicosSelecionadosIds.Clear();
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #region Métodos
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
        public void RemoverJanela()
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private void cmbServicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                listviewSerivosAdicionados.Visibility = Visibility.Visible;

                if (cmbServicos.SelectedItem != null)
                {
                    var servico = cmbServicos.SelectedItem as Servico;

                    if (!servicosAdicionados.Contains(servico))
                    {
                        servicosAdicionados.Add(servico);
                    }

                    listviewSerivosAdicionados.ItemsSource = servicosAdicionados;
                }
            }

        }

        private void btnRemoveServico_Click(object sender, RoutedEventArgs e)
        {
            Button btnRemoverServico = sender as Button;
            Servico servicoParaRemover = btnRemoverServico.DataContext as Servico;
            servicosAdicionados.Remove(servicoParaRemover);

            if(servicosAdicionados.Count == 0)
            {
                listviewSerivosAdicionados.Visibility = Visibility.Collapsed;
            }
        }
    }
}
