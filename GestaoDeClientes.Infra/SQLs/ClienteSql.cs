using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class ClienteSql
    {
        public static string Insert = @"
            INSERT INTO Cliente(
                Id,
                Nome,
                Email,
                Telefone,
                DataNascimento,
                Endereco,
                Ativo
            ) VALUES (
                @Id,
                @Nome,
                @Email,
                @Telefone,
                @DataNascimento,
                @Endereco,
                @Ativo
            );";

        public static string Update = @"
            UPDATE Cliente SET
                Nome = @Nome,
                Email = @Email,
                Telefone = @Telefone,
                DataNascimento = @DataNascimento,
                Endereco = @Endereco,
                Ativo = @Ativo
            WHERE Id = @Id;";

        public static string Delete = @"Remove from Cliente where Id = @Id";

        public static string GetById = @"SELECT * FROM Cliente WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Cliente";

        public static string GetByNome = @"SELECT * FROM Cliente WHERE Nome = @Nome";
    }


}
