using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.SQLs;
using Microsoft.Data.Sqlite;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.CodeDom;
using System.Windows;
using GestaoDeClientes.Domain;

namespace GestaoDeClientes.Infra.Repositories
{
    public class ServicoRepository : IRepository<Servico>
    {
        string connString = string.Format("Data Source={0}", Util.Util.GetDbFilePath());
        public async Task AddAsync(Servico Servico)
        {
            var verificarNomeServico = await VerifyNomeExist(Servico.Nome);

            if (verificarNomeServico)
                throw new Exception("Já existe um Servico com esse nome");


            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ServicoSql.Insert, new
                {
                    Id = Servico.Id,
                    Nome = Servico.Nome,
                    Descricao = Servico.Descricao,
                    Valor = Servico.Valor,
                    Ativo = Servico.Ativo
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
                    connection.Execute(ServicoSql.Delete, new
                    {
                        Id = id
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<IEnumerable<Servico>> GetAllAsync()
        {

            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.Query<Servico>(ServicoSql.GetAll);
            }
        }

        public Task<Servico> GetByNomeAsync(string nome)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<Servico>(ServicoSql.GetByNome, new
                {
                    Nome = nome
                });
            }
        }

        public Task<Servico> GetByIdAsync(string id)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<Servico>(ServicoSql.GetById, new
                {
                    Id = id
                });
            }
        }

        public async Task UpdateAsync(Servico Servico)
        {
            var verificarNomeExiste = await VerifyNomeExist(Servico.Nome, Servico.Id.ToString());

            if (verificarNomeExiste)
                throw new Exception("Já existe um Servico com esse nome");

            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ServicoSql.Update, new
                {
                    Id = Servico.Id,
                    Nome = Servico.Nome,
                    Descricao = Servico.Descricao,
                    Valor = Servico.Valor,
                    Ativo = Servico.Ativo
                });
            }
        }

        public async Task<bool> VerifyNomeExist (string nome)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                var result = await connection.QueryAsync<Servico>(ServicoSql.GetByNome, new
                {
                    Nome = nome
                });
                return result.Any();
            }
        }

        public async Task<bool> VerifyNomeExist(string nome, string ServicoId)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();

                var result = await connection.QueryAsync<Servico>(ServicoSql.GetByNomeAndNotId, new
                {
                    Nome = nome,
                    Id = ServicoId
                });

                return result.Any();
            }
        }

    }
}
