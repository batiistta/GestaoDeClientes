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
    /// Interação lógica para DetalhesServicoView.xam
    /// </summary>
    public partial class DetalhesServicoView : UserControl, IRemoverJanela
    {
        #region Propriedades
        public event EventHandler OnCancelarClicado;
        ServicoRepository ServicoRepository = new ServicoRepository();
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();
        private Servico _Servico;

        #endregion

        #region Construtores
        public DetalhesServicoView()
        {
            InitializeComponent();
        }

        public DetalhesServicoView(Servico Servico)
        {
            InitializeComponent();
            this.DataContext = Servico;
            _Servico = Servico;
        }
        #endregion

        #region Eventos
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtNomeServico.Focus();
            btnAtualizar.IsEnabled = false;
        }

        private void txtText_textChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        #endregion

        #region Botões
        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomeServico.Text.Any())
            {
                _Servico.Nome = txtNomeServico.Text.ToUpper();
            }

            if (ServicoAtivo.IsChecked == false)
            {
                _Servico.Ativo = false;
            }
            try
            {
                await ServicoRepository.UpdateAsync(_Servico);

                GCMessageBox.Show("Servico atualizado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                ServicoView ServicoView = new ServicoView();
                this.Content = ServicoView;
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Error", GCMessageBox.MessageBoxStatus.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServicoView ServicoView = new ServicoView();
                this.Content = ServicoView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Métodos
        public void RemoverJanela()
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        private void VerificarErrosEBloquearBotao()
        {
            bindingExpressions.Add(txtNomeServico.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtDescricaoServico.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtQuantidadeServico.GetBindingExpression(TextBox.TextProperty));
            bool algumErro = bindingExpressions.Any(x =>
            {
                x?.UpdateSource();
                return x?.HasError == true;
            });
            btnAtualizar.IsEnabled = !algumErro;
            bindingExpressions.Clear();
        }
        private void ResetBindingExpressions(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

                bindingExpression?.UpdateTarget();
            }
        }
        #endregion
    }
}
