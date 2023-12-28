using GestaoDeClientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Shared
{
    public class Global
    {
        public Usuario UsuarioAutenticado { get; set; }

        private static Global _global;

        public static Global Instance
        {
            get
            {
                if (_global == null)
                {
                    _global = new Global();
                    _global.UsuarioAutenticado = new Usuario();
                }
                return _global;
            }

        }
    }
}
