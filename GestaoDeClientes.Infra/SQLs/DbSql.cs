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
            DataAgendamento DATETIME NOT NULL,
            IdCliente TEXT,
            IdProduto TEXT,
            NomeCliente TEXT,
            NomeProduto TEXT,
            FOREIGN KEY (IdCliente) REFERENCES Cliente(Id),
            FOREIGN KEY (IdProduto) REFERENCES Produto(Id)
        );
            
            CREATE TABLE IF NOT EXISTS Usuario(
                Id TEXT PRIMARY KEY,
                Login VARCHAR(100) NOT NULL,
                Senha VARCHAR(100) NOT NULL,
                Nome VARCHAR(200),
                Email VARCHAR(200),
                DataCadastro DATETIME,
                Ativo BIT
            );

            INSERT OR IGNORE INTO Usuario (Id, Login, Senha, Nome, Email, DataCadastro, Ativo)
            VALUES ('1', 'admin', 'admin', 'Administrador', NULL, DATETIME('now'), 1);
        );"
        ;       

    }
}