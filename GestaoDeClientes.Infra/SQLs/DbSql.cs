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
                Email VARCHAR(200) ,
                Telefone VARCHAR(30) ,
                DataNascimento DATETIME ,
                Endereco VARCHAR(200) ,
                Ativo BIT 
            );
            
            CREATE TABLE IF NOT EXISTS Produto(
                Id TEXT PRIMARY KEY,
                Nome VARCHAR(200) NOT NULL ,
                Descricao VARCHAR(200) ,
                ValorCompra DECIMAL(10,2) ,
                ValorUnitario DECIMAL(10,2) ,
                Ativo BIT 
            );
            
            CREATE TABLE IF NOT EXISTS AGENDAMENTO(
                Id TEXT PRIMARY KEY,
                IdCliente TEXT NOT NULL,
                DataAgendamento DATETIME NOT NULL ,
                ValorTotal DECIMAL(10,2) NOT NULL ,
                ValorCusto DECIMAL(10,2) NOT NULL ,
                Observacao VARCHAR(200) ,   
                Lucro DECIMAL(10,2) AS (ValorTotal - ValorCusto) PERSISTED,
                Ativo BIT 
            );"
        ;

    }
}
