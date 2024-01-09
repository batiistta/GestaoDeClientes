using GestaoDeClientes.Domain.Models;
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
    /// Interação lógica para DetalhesProdutoView.xam
    /// </summary>
    public partial class DetalhesProdutoView : UserControl
    {
        public DetalhesProdutoView()
        {
            InitializeComponent();
        }

        public DetalhesProdutoView(Produto produto)
        {
            InitializeComponent();
            txtNomeProduto.Text = produto.Nome;
            txtDescricaoProduto.Text = produto.Descricao;
            txtValorCompraProduto.Text = produto.ValorCompra.ToString();
            txtValorUnitarioProduto.Text = produto.ValorUnitario.ToString();
            txtQuantidadeProduto.Text = produto.Quantidade.ToString();            
        }
    }
}
