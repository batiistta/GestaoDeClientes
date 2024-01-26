using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Domain.Models
{
    public class Agendamento
    {
        public string Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string IdCliente { get; set; }   
        public string NomeCliente { get; set; }
        public string IdServico { get; set; }
        public string NomeServico { get; set; }
        public List<string> IdsServicos { get; set; }
    }
}
