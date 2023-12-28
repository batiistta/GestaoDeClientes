using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Domain.Models
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime DataAgendamento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorCusto { get; set; }        
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
        public decimal Lucro
        {
            get
            {
                return ValorTotal - ValorCusto;
            }
            set
            {
                Lucro = ValorTotal - ValorCusto;
            }
        }
    }
}
