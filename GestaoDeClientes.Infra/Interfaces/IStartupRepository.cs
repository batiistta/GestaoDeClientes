using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.Interfaces
{
    public interface IStartupRepository
    {
        Task<bool> VerifyDatabase();
    }
}
