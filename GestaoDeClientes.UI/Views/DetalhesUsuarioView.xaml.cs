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
    /// Interação lógica para DetalhesUsuarioView.xam
    /// </summary>
    public partial class DetalhesUsuarioView : UserControl, IRemoverJanela
    {
        public event EventHandler OnCancelarClicado;
        public Usuario _usuario;
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();
        public DetalhesUsuarioView()
        {
            InitializeComponent();
        }

        public DetalhesUsuarioView(Usuario usuario)
        {
            InitializeComponent();
            this.DataContext = usuario;
            _usuario = usuario;            
        }
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtLogin.Focus();
            btnAtualizar.IsEnabled = false;
        }
        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await usuarioRepository.UpdateAsync(_usuario);
                GCMessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
                GCMessageBox.Show("Erro ao atualizar usuário!", "Erro", GCMessageBox.MessageBoxStatus.Error);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }       
        private void txtLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextOnly(e.Text))
            {
                e.Handled = true;
            }
        }
        private void txtNome_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextOnly(e.Text))
            {
                e.Handled = true;
            }
        }
        private void txtSenha_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerificarErrosEBloquearBotao();
        }
        private bool IsTextOnly(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }
        private void VerificarErrosEBloquearBotao()
        {
            bindingExpressions.Add(txtLogin.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtNome.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtEmail.GetBindingExpression(TextBox.TextProperty));
            bindingExpressions.Add(txtSenha.GetBindingExpression(TextBox.TextProperty));
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
        private void Limpar()
        {
            ResetBindingExpressions(txtNome, txtLogin, txtSenha, txtEmail);
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        public void RemoverJanela()
        {
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
    }
}
