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
                    IdServico = entity.IdServico,
                    IdsServicos = entity.IdsServicos.ToString(),
                    DataAgendamento = entity.DataAgendamento,
                    NomeCliente = entity.NomeCliente,
                    NomeServico = entity.NomeServico
                });

                foreach (var idServico in entity.IdsServicos)
                {
                    connection.Execute(AgendamentoServicoSql.Insert, new
                    {
                        IdAgendamento = entity.Id,
                        IdServico = idServico
                    });
                }
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                IEnumerable<Agendamento> agendamentos = new List<Agendamento>();
                using (var connection = new SqliteConnection(connString))
                {
                    SQLitePCL.Batteries.Init();
                    connection.Open();
                   agendamentos = connection.Query<Agendamento>(AgendamentoSql.GetAll);
                }
                foreach (var agendamento in agendamentos)
                {
                    if (agendamento.Id == id)
                    {
                        foreach (var idServico in agendamento.IdsServicos)
                        {
                            using (var connection = new SqliteConnection(connString))
                            {
                                SQLitePCL.Batteries.Init();
                                connection.Open();
                                connection.Execute(AgendamentoServicoSql.Delete, new { IdAgendamento = id });
                            }
                        }
                    }
                }
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
                        IdServico = entity.IdServico,
                        DataAgendamento = entity.DataAgendamento,
                        NomeCliente = entity.NomeCliente,
                        NomeServico = entity.NomeServico
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
