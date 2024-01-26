using GestaoDeClientes.Domain;
using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.Repositories;
using GestaoDeClientes.UI.Popup;
using System;
using System.Collections.Generic;
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
        public List<string> ServicosSelecionadosIds { get; set; } = new List<string>();

        private void AddServico(string servicoId)
        {
            if (!ServicosSelecionadosIds.Contains(servicoId))
                ServicosSelecionadosIds.Add(servicoId);
        }

        #region Propriedades
        public event EventHandler OnCancelarClicado;
        ClienteRepository clienteRepository = new ClienteRepository();
        ServicoRepository ServicoRepository = new ServicoRepository();
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
                CarregarServicos();
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
                agendamento.IdsServicos = ServicosSelecionadosIds;

                if (cmbServicos.SelectedItem != null)
                {
                    agendamento.NomeServico = (cmbServicos.SelectedItem as Servico).Nome.ToUpper();
                    agendamento.IdServico = (cmbServicos.SelectedItem as Servico).Id;                    

                }
                else
                {
                    agendamento.NomeServico = null;
                    agendamento.IdServico = null;
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

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string servicoId)
            {
                AddServico(servicoId);

                btn.IsEnabled = false;

                var btnRemover = GetBtnRemoverFromBtnAdicionar(btn);
                if (btnRemover != null)
                {
                    btnRemover.IsEnabled = true;
                }
            }
        }

        private void btnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string servicoId)
            {
                ServicosSelecionadosIds.Remove(servicoId);

                var btnAdicionar = GetBtnAdicionarFromBtnRemover(btn);
                if (btnAdicionar != null)
                {
                    btnAdicionar.IsEnabled = true;
                }

                btn.IsEnabled = false;
            }
        }

        private Button GetBtnRemoverFromBtnAdicionar(Button btnAdicionar)
        {
            var parentStackPanel = (StackPanel)btnAdicionar.Parent;
            var btnRemover = parentStackPanel.Children.OfType<Button>().FirstOrDefault(b => b.Name == "btnRemover");

            return btnRemover;
        }

        private Button GetBtnAdicionarFromBtnRemover(Button btnRemover)
        {
            var parentStackPanel = (StackPanel)btnRemover.Parent;
            var btnAdicionar = parentStackPanel.Children.OfType<Button>().FirstOrDefault(b => b.Name == "btnAdicionar");

            return btnAdicionar;
        }


    }
}
