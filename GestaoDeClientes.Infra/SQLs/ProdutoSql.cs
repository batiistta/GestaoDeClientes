using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class ProdutoSql
    {
        public static string Insert = @"
            INSERT INTO Produto(
                Id,
                Nome,
                Descricao,
                ValorUnitario,
                ValorCompra,
                Quantidade,
                Ativo
            ) VALUES (
                @Id,
                @Nome,
                @Descricao,
                @ValorUnitario,
                @ValorCompra,
                @Quantidade,
                @Ativo
            );";

        public static string Update = @"
            UPDATE Produto SET
                Nome = @Nome,
                Descricao = @Descricao,
                ValorUnitario = @ValorUnitario,
                ValorCompra = @ValorCompra,
                Quantidade = @Quantidade,
                Ativo = @Ativo
            WHERE Id = @Id;";

        public static string Delete = @"Remove from Produto where Id = @Id";

        public static string GetById = @"SELECT * FROM Produto WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Produto";

        public static string GetByNome = @"SELECT * FROM Produto WHERE Nome = @Nome";
    }
}
