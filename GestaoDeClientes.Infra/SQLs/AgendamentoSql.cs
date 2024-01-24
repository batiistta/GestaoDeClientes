using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class AgendamentoSql
    {
        public static string Insert = @"
            INSERT INTO Agendamento(
                Id,
                IdCliente,
                IdProduto,
                DataAgendamento,
                NomeCliente,
                NomeProduto
            ) VALUES (
                @Id,
                @IdCliente,
                @IdProduto,
                @DataAgendamento,
                @NomeCliente,
                @NomeProduto
            );";



        public static string Delete = @"DELETE FROM Agendamento WHERE Id = @Id";

        public static string GetById = @"SELECT * FROM Agendamento WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Agendamento";

        public static string GetByNome = @"SELECT * FROM Agendamento WHERE Nome = @Nome";
    }
}
