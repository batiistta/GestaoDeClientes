using GestaoDeClientes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GestaoDeClientes.Infra.SQLs;
using Microsoft.Data.Sqlite;
using GestaoDeClientes.Infra.Interfaces;

namespace GestaoDeClientes.Infra.Repositories
{
    public class AgendamentoRepository : IRepository<Agendamento>
    {
        private string dbPath = Util.Util.GetDbFilePath();
        public async Task AddAsync(Agendamento agendamento)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(AgendamentoSql.Insert, new
                {
                    Id = agendamento.Id,
                    IdCliente = agendamento.IdCliente,
                    DataAgendamento = agendamento.DataAgendamento
                });

            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(AgendamentoSql.Delete, new
                {
                    Id = id
                });

            }
        }

        public async Task<IEnumerable<Agendamento>> GetAllAsync()
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.Query<Agendamento>(AgendamentoSql.GetAll);
            }
        }

        public async Task<Agendamento> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefault<Agendamento>(AgendamentoSql.GetById, new
                {
                    Id = id
                });
            }
        }

        public async Task<Agendamento> GetByNomeAsync(string nome)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefault<Agendamento>(AgendamentoSql.GetByNome, new
                {
                    Nome = nome
                });
            }
        }

        public async Task UpdateAsync(Agendamento entity)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(AgendamentoSql.Update, new
                {
                    Id = entity.Id,
                    IdCliente = entity.IdCliente,
                    DataAgendamento = entity.DataAgendamento
                });
            }
        }
    }
}
