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
                DataAgendamento,
                NomeCliente
            ) VALUES (
                @Id,
                @IdCliente,
                @DataAgendamento,
                @NomeCliente
            );";


        public static string Update = @"
            UPDATE Agendamento SET
                IdCliente = @IdCliente,
                DataAgendamento = @DataAgendamento,
                NomeCliente = @NomeCliente
            WHERE Id = @Id;";

        public static string Delete = @"DELETE FROM Agendamento WHERE Id = @Id";

        public static string GetById = @"SELECT * FROM Agendamento WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Agendamento";

        public static string GetByNome = @"SELECT * FROM Agendamento WHERE Nome = @Nome";
    }
}
