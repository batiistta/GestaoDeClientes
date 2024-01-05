using GestaoDeClientes.Domain;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Util;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDeClientes.Infra.SQLs;

namespace GestaoDeClientes.Infra.Repositories
{
    public class ClienteRepository : IRepository<Cliente>
    {
        string connString = string.Format("Data Source={0}", Util.Util.GetDbFilePath());
        public async Task AddAsync(Cliente cliente)
        {            
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ClienteSql.Insert, new
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    DataNascimento = cliente.DataNascimento,
                    DataCadastro = cliente.DataCadastro,
                    Endereco = cliente.Endereco,
                    Ativo = cliente.Ativo
                });

            }
        }

        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ClienteSql.Delete, new
                {
                    Id = id
                });

            }
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.Query<Cliente>(ClienteSql.GetAll);
            }
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.Query<Cliente>(ClienteSql.GetById, new
                {
                    Id = id
                }).FirstOrDefault();
            };
        }


        public async Task<Cliente> GetByNomeAsync(string nome)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                var result = await connection.QueryAsync<Cliente>(ClienteSql.GetByNome, new
                {
                    Nome = nome
                });
                return result.FirstOrDefault();
            }
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ClienteSql.Update, new
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    DataNascimento = cliente.DataNascimento,
                    DataCadastro = cliente.DataCadastro,
                    Endereco = cliente.Endereco,
                    Ativo = cliente.Ativo
                });
            }
        }
    }    
}
