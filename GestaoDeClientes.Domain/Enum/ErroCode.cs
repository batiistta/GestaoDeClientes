using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Domain.Enum
{
    public enum ErroCode
    {
        RequestError,
        ExternalServiceUnavailable,
        InsufficientPermissions,
        InvalidCredentials,
        UnauthorizedAuthentication
    }
}
