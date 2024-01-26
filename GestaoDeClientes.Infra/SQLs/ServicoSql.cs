using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class ServicoSql
    {
        public static string Insert = @"
            INSERT INTO Servico(
                Id,
                Nome,
                Descricao,
                Valor,
                Ativo
            ) VALUES (
                @Id,
                @Nome,
                @Descricao,
                @Valor,
                @Ativo
            );";

        public static string Update = @"
            UPDATE Servico SET
                Nome = @Nome,
                Descricao = @Descricao,
                Valor = @Valor,
                Ativo = @Ativo
            WHERE Id = @Id;";

        public static string Delete = @"DELETE from Servico where Id = @Id";

        public static string GetById = @"SELECT * FROM Servico WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Servico";

        public static string GetByNome = @"SELECT * FROM Servico WHERE Nome = @Nome";

        public static string GetByNomeAndNotId = @"SELECT * FROM Servico WHERE Nome = @Nome AND Id <> @Id;";
    }
}
