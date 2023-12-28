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

namespace GestaoDeClientes.Infra.Repositories
{
    public class ProdutoRepository : IRepository<Produto>
    {
        private string dbPath = Util.Util.GetDbFilePath();
        public async Task AddAsync(Produto produto)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ProdutoSql.Insert, new
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    ValorUnitario = produto.ValorUnitario,
                    ValorCompra = produto.ValorCompra,
                    Quantidade = produto.Quantidade,
                    Ativo = produto.Ativo
                });
            }
        }

        public async Task DeleteAsync(Produto produto)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ProdutoSql.Delete, new
                {
                    Id = produto.Id
                });
            }
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.Query<Produto>(ProdutoSql.GetAll);
            }
        }

        public Task<Produto> GetByNomeAsync(string nome)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<Produto>(ProdutoSql.GetByNome, new
                {
                    Nome = nome
                });
            }
        }

        public Task<Produto> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<Produto>(ProdutoSql.GetById, new
                {
                    Id = id
                });
            }
        }

        public async Task UpdateAsync(Produto produto)
        {
            using (var connection = new SqliteConnection("Data Source" + dbPath))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(ProdutoSql.Update, new
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    ValorUnitario = produto.ValorUnitario,
                    ValorCompra = produto.ValorCompra,
                    Quantidade = produto.Quantidade,
                    Ativo = produto.Ativo
                });
            }
        }
    }
}
