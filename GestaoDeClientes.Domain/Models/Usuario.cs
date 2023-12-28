using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Domain.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set;}
        public string Email { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
