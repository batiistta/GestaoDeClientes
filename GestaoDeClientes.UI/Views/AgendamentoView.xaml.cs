using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
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
    /// Interação lógica para AgendamentoView.xam
    /// </summary>
    public partial class AgendamentoView : UserControl
    {
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        CadastrarAgendamentoView cadastrarAgendamentoView = new CadastrarAgendamentoView();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        List<Agendamento> agendamentos = new List<Agendamento>();
        public AgendamentoView()
        {
            InitializeComponent();
        }


        private void CadastrarAgendamentoView_OnCancelarClicado(object sender, EventArgs e)
        {
            primeiraGrid.Children.Remove(cadastrarAgendamentoView);
            gridPrincipal.IsEnabled = true;
        }

        private void btnCadastrarAgendamento_Click(object sender, RoutedEventArgs e)
        {
            primeiraGrid.Children.Add(cadastrarAgendamentoView);
            cadastrarAgendamentoView.OnCancelarClicado += CadastrarAgendamentoView_OnCancelarClicado;
            gridPrincipal.IsEnabled = false;
        }

        private void RemoverJanelasFilhas()
        {
            foreach (var child in primeiraGrid.Children.OfType<IRemoverJanela>().ToList())
            {
                child.RemoverJanela();
            }

            gridPrincipal.IsEnabled = true;
        }
        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsVisible)
            {
                RemoverJanelasFilhas();
            }
        }

        private void listViewAgendamentos_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarAgendamentos();
            }
        }

        private void CarregarAgendamentos()
        {
            agendamentos = agendamentoRepository.GetAllAsync().Result.ToList();
            listViewAgendamentos.ItemsSource = agendamentos;
        }
    }
}
