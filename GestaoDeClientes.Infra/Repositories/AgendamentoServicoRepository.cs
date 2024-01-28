using Dapper;
using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.SQLs;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.Repositories
{
    public class AgendamentoServicoRepository
    {
        string connString = string.Format("Data Source={0}", Util.Util.GetDbFilePath());

        public async Task AddAsync(string idAgendamento, string idServico)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(AgendamentoServicoSql.Insert, new
                {
                    IdAgendamento = idAgendamento,
                    IdServico = idServico
                });
            }
        }

        public Task<IEnumerable<AgendamentoServico>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return Task.FromResult(connection.Query<AgendamentoServico>(AgendamentoServicoSql.GetAll));
            }
        }

        public Task UpdateAsync(AgendamentoServico agendamentoServico)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(AgendamentoServicoSql.Update, new
                {
                    IdAgendamento = agendamentoServico.IdAgendamento,
                    IdServico = agendamentoServico.IdServico
                });
            }
            return Task.CompletedTask;
        }
    }
}
