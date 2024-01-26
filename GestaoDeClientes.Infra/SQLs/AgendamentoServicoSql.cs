using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class AgendamentoServicoSql
    {
        public static string Insert = @"
        INSERT INTO Agendamento_Servico (
            IdAgendamento,
            IdServico
        ) VALUES (
            @IdAgendamento,
            @IdServico
        );";

        public static string Update = @"
        UPDATE Agendamento_Servico SET
            IdAgendamento = @IdAgendamento,
            IdServico = @IdServico
        WHERE IdAgendamento = @IdAgendamento AND IdServico = @IdServico;";

        public static string Delete = @"DELETE from Agendamento_Servico where IdAgendamento = @Id";

        public static string GetById = @"SELECT * FROM Agendamento_Servico WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Agendamento_Servico";
    }
}
