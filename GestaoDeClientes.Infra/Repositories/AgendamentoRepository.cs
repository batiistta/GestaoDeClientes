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
using GestaoDeClientes.Domain;

namespace GestaoDeClientes.Infra.Repositories
{
    public class AgendamentoRepository : IRepository<Agendamento>
    {
        string connString = string.Format("Data Source={0}", Util.Util.GetDbFilePath());


        public async Task AddAsync(Agendamento entity)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(AgendamentoSql.Insert, new
                {
                    Id = entity.Id,
                    IdCliente = entity.IdCliente,
                    IdProduto = entity.IdProduto,
                    DataAgendamento = entity.DataAgendamento,
                    NomeCliente = entity.NomeCliente,
                    NomeProduto = entity.NomeProduto
                });
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                using (var connection = new SqliteConnection(connString))
                {
                    SQLitePCL.Batteries.Init();
                    connection.Open();
                    connection.Execute(AgendamentoSql.Delete, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<IEnumerable<Agendamento>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return Task.FromResult(connection.Query<Agendamento>(AgendamentoSql.GetAll));
            }
        }

        public Task<Agendamento> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Agendamento> GetByNomeAsync(string nome)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Agendamento entity)
        {
            try
            {
                using (var connection = new SqliteConnection(connString))
                {
                    SQLitePCL.Batteries.Init();
                    connection.Open();
                    connection.Execute(AgendamentoSql.Update, new
                    {
                        Id = entity.Id,
                        IdCliente = entity.IdCliente,
                        IdProduto = entity.IdProduto,
                        DataAgendamento = entity.DataAgendamento,
                        NomeCliente = entity.NomeCliente,
                        NomeProduto = entity.NomeProduto
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
