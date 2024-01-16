using GestaoDeClientes.Domain.Models;
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
    /// Interação lógica para CadastrarUsuarioView.xam
    /// </summary>
    public partial class CadastrarUsuarioView : UserControl
    {
        public event EventHandler OnCancelarClicado;
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        List<BindingExpression> bindingExpressions = new List<BindingExpression>();

        public CadastrarUsuarioView()
        {
            InitializeComponent();
            this.DataContext = new Usuario();
        }

        private async void btnCadastrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Id = Guid.NewGuid().ToString();
                usuario.Nome = txtNome.Text;
                usuario.Login = txtLogin.Text;
                usuario.Senha = txtSenha.Text;
                usuario.Email = txtEmail.Text;
                usuario.DataCadastro = DateTime.Now;
                usuario.Ativo = true;

                await usuarioRepository.AddAsync(usuario);
                GCMessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", GCMessageBox.MessageBoxStatus.Ok);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                GCMessageBox.Show(ex.Message, "Erro", GCMessageBox.MessageBoxStatus.Error);
                OnCancelarClicado?.Invoke(this, EventArgs.Empty);
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpar();
            OnCancelarClicado?.Invoke(this, EventArgs.Empty);
        }
        private void Limpar()
        {
            ResetBindingExpressions(txtNome, txtLogin, txtSenha, txtEmail);
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtLogin.Focus();
            Limpar();
            btnCadastrar.IsEnabled = false;
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
            btnCadastrar.IsEnabled = !algumErro;
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
    }
}
