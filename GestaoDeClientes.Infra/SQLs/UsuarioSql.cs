﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoDeClientes.Infra.SQLs
{
    public static class UsuarioSql
    {
            public static string Insert = @"
            INSERT INTO Usuario(
                Id,
                Login,
                Senha,
                Nome,
                Email,
                DataCadastro
            ) VALUES (
                @Id,
                @Login,
                @Senha,
                @Nome,
                @Email,
                @DataCadastro
            );";    

        public static string Update = @"
            UPDATE Usuario SET
                Login = @Login,
                Senha = @Senha,
                Nome = @Nome,
                Email = @Email,
                DataCadastro = @DataCadastro
            WHERE Id = @Id;";

        public static string Delete = @"Remove from Usuario where Id = @Id";

        public static string GetById = @"SELECT * FROM Usuario WHERE Id = @Id";

        public static string GetAll = @"SELECT * FROM Usuario";

        public static string GetByNome = @"SELECT * FROM Usuario WHERE Nome = @Nome";
    }
}
