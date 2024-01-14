﻿using GestaoDeClientes.Domain.Models;
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
                    DataAgendamento = entity.DataAgendamento
                });
            }
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Agendamento>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Agendamento> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Agendamento> GetByNomeAsync(string nome)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Agendamento entity)
        {
            throw new NotImplementedException();
        }
    }
}
