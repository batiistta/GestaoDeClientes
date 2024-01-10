using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class DbSql
    {
        public static string CreateDatabase = @"
            CREATE TABLE IF NOT EXISTS Cliente(
                Id TEXT PRIMARY KEY,
                Nome VARCHAR(200) NOT NULL ,
                Endereco VARCHAR(200) ,
                Telefone VARCHAR(30) ,
                DataNascimento DATETIME ,
                DataCadastro DATETIME ,
                Ativo BIT 
            );
            
            CREATE TABLE IF NOT EXISTS Produto(
                Id TEXT PRIMARY KEY,
                Nome VARCHAR(200) NOT NULL ,
                Descricao VARCHAR(200) ,
                ValorCompra DECIMAL(10,2) ,
                ValorUnitario DECIMAL(10,2) ,
                Quantidade INT,
                Ativo BIT 
            );
            
            CREATE TABLE IF NOT EXISTS Agendamento(
                Id TEXT PRIMARY KEY,
                IdCliente TEXT NOT NULL,
                DataAgendamento DATETIME NOT NULL ,
                ValorTotal DECIMAL(10,2) NOT NULL ,
                ValorCusto DECIMAL(10,2) NOT NULL ,
                Observacao VARCHAR(200) ,   
                Lucro DECIMAL(10, 2) NOT NULL,
                Ativo BIT 
            );
            
            CREATE TABLE IF NOT EXISTS Usuario(
                Id TEXT PRIMARY KEY,
                Login VARCHAR(100) NOT NULL,
                Senha VARCHAR(100) NOT NULL,
                Nome VARCHAR(200),
                Email VARCHAR(200),
                DataCadastro DATETIME
            );

            INSERT OR IGNORE INTO Usuario (Id, Login, Senha, Nome, Email, DataCadastro)
            VALUES ('1', 'admin', 'admin', 'Administrador', NULL, DATETIME('now'));
        
            CREATE TABLE IF NOT EXISTS Agendamento(
                Id TEXT PRIMARY KEY,
                IdCliente TEXT NOT NULL,
                DataAgendamento DATETIME NOT NULL ,
                ValorTotal DECIMAL(10,2) NOT NULL ,
                ValorCusto DECIMAL(10,2) NOT NULL ,
                Observacao VARCHAR(200) ,   
                Lucro DECIMAL(10, 2) NOT NULL,
                Ativo BIT 
        );"
        ;       

    }
}