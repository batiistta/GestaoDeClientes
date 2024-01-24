﻿using GestaoDeClientes.Domain.Models;
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
        public event EventHandler ChildWindowClosed;
        public event EventHandler OnCancelarClicado;
        CadastrarAgendamentoView cadastrarAgendamentoView = new CadastrarAgendamentoView();
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        DetalhesAgendamento detalhesAgendamentoView = new DetalhesAgendamento();
        List<Agendamento> agendamentos = new List<Agendamento>();
        public AgendamentoView()
        {
            InitializeComponent();
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
            try
            {
                Button btnDeletar = sender as Button;

                Agendamento agendamentoParaDeletar = btnDeletar.DataContext as Agendamento;

                if (MessageBox.Show("Deseja realmente excluir o agendamento?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    agendamentoRepository.DeleteAsync(agendamentoParaDeletar.Id);
                    CarregarAgendamentos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, GCMessageBox.MessageBoxStatus.Error);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.IsVisible)
            {
                RemoverJanelasFilhas();
            }
            if (this.IsVisible)
            {
                CarregarAgendamentos();
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
