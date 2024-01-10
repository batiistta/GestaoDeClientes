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
    /// Interação lógica para UsuarioView.xam
    /// </summary>
    public partial class UsuarioView : UserControl
    {
        public UsuarioView()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                CarregarUsuarios();
            }
        }

        private void CarregarUsuarios()
        {
            try
            {
                UsuarioRepository usuarioRepository = new UsuarioRepository();
                var usuarios = usuarioRepository.GetAll();
                listViewUsuarios.ItemsSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }
    }
}
