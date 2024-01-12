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
        public DateTime DataAgendamento { get; set; }
        public string IdCliente { get; set; }
    }
}
