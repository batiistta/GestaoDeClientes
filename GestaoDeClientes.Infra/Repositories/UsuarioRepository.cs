using Dapper;
using GestaoDeClientes.Domain.Models;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.SQLs;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        string connString = string.Format("Data Source={0}", Util.Util.GetDbFilePath());

        public async Task AddAsync(Usuario usuario)
        {
            var existeUsuario = await VerifyUsuarioExist(usuario.Login);

            if (existeUsuario)
                throw new Exception("Já existe um usuário cadastrado");

            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(UsuarioSql.Insert, new
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataCadastro = usuario.DataCadastro,
                    Ativo = usuario.Ativo
                });
            }
        }

        public bool ValidarUsuario(string login, string senha)
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Usuario WHERE Login = @Login AND Senha = @Senha;";
                var count = connection.ExecuteScalar<int>(query, new { Login = login, Senha = senha });

                return count > 0;
            }
        }

        public bool isAtivo(string login)
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Usuario WHERE Login = @Login AND Ativo = 1;";
                var count = connection.ExecuteScalar<int>(query, new { Login = login });

                return count > 0;
            }
        }

        public Task<Usuario> GetByNomeAsync(string username)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<Usuario>(UsuarioSql.GetByNome, new
                {
                    Nome = username
                });
            }
        }

        public Usuario GetByNome(string username)
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                return connection.Query<Usuario>(UsuarioSql.GetByLogin, new { Login = username }).FirstOrDefault();
            }

        }

        public Task<Usuario> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<Usuario>(UsuarioSql.GetById, new
                {
                    Id = id
                });
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                return connection.Query<Usuario>(UsuarioSql.GetAll);
            }
        }
        public async Task DeleteAsync(string id)
        {
            using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                connection.Execute(UsuarioSql.Delete, new { Id = id });
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                connection.Execute(UsuarioSql.Update, new
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataCadastro = usuario.DataCadastro,
                    Ativo = usuario.Ativo
                });
            }
        }

        public async Task<bool> VerifyUsuarioExist (string login)
        {
           using (var connection = new SqliteConnection(connString))
            {
                SQLitePCL.Batteries.Init();
                connection.Open();
                var result = await connection.QueryAsync<Usuario>(UsuarioSql.GetByLogin, new
                {
                    Login = login
                });
                return result.Any();
            }
        }
    }
}
