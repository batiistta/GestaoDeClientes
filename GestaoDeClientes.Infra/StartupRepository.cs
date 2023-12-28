using Dapper;
using GestaoDeClientes.Infra.Interfaces;
using GestaoDeClientes.Infra.SQLs;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra
{
    public class StartupRepository : IStartupRepository
    {
        public async Task<bool> VerifyDatabase()
        {
            try
            {
                string path = Util.Util.GetDbFilePath();
                FileInfo DbFile = new FileInfo(path);

                if (!File.Exists(path))
                {
                    if (!await CreateDatabase())
                        return false;
                }

                if (DbFile.Length <= 0)
                {
                    await CreateDatabaseTables();
                }

                return true;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CreateDatabase()
        {
            try
            {
                StreamWriter file = new StreamWriter(Util.Util.GetDbFilePath(), true, System.Text.Encoding.Default);

                file.Dispose();

                if (!await CreateDatabaseTables())
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CreateDatabaseTables()
        {
            try
            {
                string dbPath = Util.Util.GetDbFilePath();

                using (var connection = new SqliteConnection("Data Source=" + dbPath))
                {
                    SQLitePCL.Batteries.Init();
                    connection.Open();
                    connection.Execute(DbSql.CreateDatabase);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
