using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Domain.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorCompra { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
