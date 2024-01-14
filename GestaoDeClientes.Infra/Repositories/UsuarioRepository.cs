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
    public class UsuarioRepository
    {
        string connString = string.Format("Data Source={0}", Util.Util.GetDbFilePath());

        public async Task AddAsync(Usuario usuario)
        {
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

        public Usuario GetByUsername(string username)
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                return connection.Query<Usuario>(UsuarioSql.GetByLogin, new { Login = username }).FirstOrDefault();
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                return connection.Query<Usuario>(UsuarioSql.GetAll);
            }
        }
        public void Delete(string id)
        {
            using (var connection = new SqliteConnection(connString))
            {
                connection.Open();
                connection.Execute(UsuarioSql.Delete, new { Id = id });
            }
        }

        public void Update(Usuario usuario)
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
    }
}
