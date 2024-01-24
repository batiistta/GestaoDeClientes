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
    /// Interação lógica para AgendamentoView.xam
    /// </summary>
    public partial class AgendamentoView : UserControl
    {
        #region Propriedades
        CadastrarAgendamentoView cadastrarAgendamentoView = new CadastrarAgendamentoView();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        DetalhesAgendamento detalhesAgendamentoView = new DetalhesAgendamento();
        List<Agendamento> agendamentos = new List<Agendamento>();
        #endregion

        #region Construtores
        public AgendamentoView()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsVisible)
            {
                RemoverJanelasFilhas();
            }
        }
        private void listViewFiles_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarAgendamentos();
            }
        }
        private void DetalhesAgendamento_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(detalhesAgendamentoView);
            gridPrincipal.IsEnabled = true;
            CarregarAgendamentos();
        }
        private void CadastrarAgendamentoView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(cadastrarAgendamentoView);
            gridPrincipal.IsEnabled = true;
            CarregarAgendamentos();
        }
        #endregion

        #region Botões
        private void btnCadastrarAgendamento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                primeiraGrid.Children.Add(cadastrarAgendamentoView);
                cadastrarAgendamentoView.OnCancelarClicado += CadastrarAgendamentoView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                cadastrarAgendamentoView.OnCancelarClicado -= CadastrarAgendamentoView_OnCancelarClicado;
                gridPrincipal.IsEnabled = false;
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }

        }
        private async void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btnDeletar = sender as Button;

                Agendamento agendamentoParaDeletar = btnDeletar.DataContext as Agendamento;

                bool confirmacao = GCMessageBox.Confirm("Deseja realmente deletar o agendamento?", "Deletar", GCMessageBox.MessageBoxStatus.Warning);

                if (confirmacao)
                {
                    await agendamentoRepository.DeleteAsync(agendamentoParaDeletar.Id);
                    CarregarAgendamentos();
                }

            }
            catch (Exception ex)
            {
                GCMessageBox.Show("Erro ao deletar agendamento", "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }
        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button buttonAtualizar = sender as Button;

                Agendamento agendamentoParaAtualizar = buttonAtualizar.DataContext as Agendamento;

                detalhesAgendamentoView = new DetalhesAgendamento(agendamentoParaAtualizar);

                primeiraGrid.Children.Add(detalhesAgendamentoView);

                detalhesAgendamentoView.OnCancelarClicado += DetalhesAgendamento_OnCancelarClicado;

                gridPrincipal.IsEnabled = false;
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            listViewAgendamentos.ItemsSource = null;
            this.Visibility = Visibility.Hidden;
        }
        private void btnBuscarAgendamentoPorNome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtBusca.Text))
                //{
                //    CarregarAgendamentos();
                //}
                //    agendamentos = agendamentos.Where(x => x.NomeCliente.ToLower().Contains(txtBusca.Text.ToLower())).ToList();
                //    listViewAgendamentos.ItemsSource = agendamentos;
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }
        #endregion

        #region Métodos
        private void CarregarAgendamentos()
        {
            agendamentos = agendamentoRepository.GetAllAsync().Result.ToList();
            listViewAgendamentos.ItemsSource = agendamentos;
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
