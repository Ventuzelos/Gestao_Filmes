using Microsoft.Data.Sqlite;
using Gestao_Filmes.Domain;
using System.Collections.Generic;

namespace Gestao_Filmes.Data
{
    public class FilmeSQLiteRepository : IFilmeRepository
    {
        private string _connectionString = "Data Source=filmes.db";

        public FilmeSQLiteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Filmes
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo TEXT NOT NULL,
                Ano INTEGER NOT NULL,
                Lingua TEXT NOT NULL,
                Classificacao REAL NOT NULL,
                CategoriaId INTEGER NOT NULL,
                RealizadorId INTEGER NOT NULL
            );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // métodos:
        public void Adicionar(Filme filme)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Filmes 
                   (Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId)
                   VALUES 
                   (@titulo, @ano, @lingua, @classificacao, @categoriaId, @realizadorId)";

            using var cmd = new SqliteCommand(sql, con);

            cmd.Parameters.AddWithValue("@titulo", filme.Titulo);
            cmd.Parameters.AddWithValue("@ano", filme.Ano);
            cmd.Parameters.AddWithValue("@lingua", filme.Lingua);
            cmd.Parameters.AddWithValue("@classificacao", filme.Classificacao);
            cmd.Parameters.AddWithValue("@categoriaId", filme.Categoria.Id);
            cmd.Parameters.AddWithValue("@realizadorId", filme.Realizador.Id);

            cmd.ExecuteNonQuery();
        }

        public Filme ProcurarPorTitulo(string titulo)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT 
                       f.Id,
                       f.Titulo,
                       f.Ano,
                       f.Lingua,
                       f.Classificacao,
                       c.Id,
                       c.Nome,
                       r.Id,
                       r.Nome,
                       r.Pais
                   FROM Filmes f
                   INNER JOIN Categorias c ON f.CategoriaId = c.Id
                   INNER JOIN Realizadores r ON f.RealizadorId = r.Id
                   WHERE f.Titulo = @titulo COLLATE NOCASE";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@titulo", titulo);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Filme
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetDecimal(4),

                    Categoria = new Categoria
                    {
                        Id = reader.GetInt32(5),
                        Nome = reader.GetString(6)
                    },

                    Realizador = new Realizador
                    {
                        Id = reader.GetInt32(7),
                        Nome = reader.GetString(8),
                        Pais = reader.GetString(9)
                    }
                };
            }

            return null;
        }

        public List<Filme> Listar()
        {
            List<Filme> lista = new List<Filme>();

            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT 
                       f.Id,
                       f.Titulo,
                       f.Ano,
                       f.Lingua,
                       f.Classificacao,
                       c.Id,
                       c.Nome,
                       r.Id,
                       r.Nome,
                       r.Pais
                   FROM Filmes f
                   INNER JOIN Categorias c ON f.CategoriaId = c.Id
                   INNER JOIN Realizadores r ON f.RealizadorId = r.Id";

            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Filme
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetDecimal(4),

                    Categoria = new Categoria
                    {
                        Id = reader.GetInt32(5),
                        Nome = reader.GetString(6)
                    },

                    Realizador = new Realizador
                    {
                        Id = reader.GetInt32(7),
                        Nome = reader.GetString(8),
                        Pais = reader.GetString(9)
                    }
                });
            }

            return lista;
        }

        public void Atualizar(Filme filme)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"UPDATE Filmes
                   SET Classificacao = @classificacao
                   WHERE Titulo = @titulo COLLATE NOCASE";

            using var cmd = new SqliteCommand(sql, con);

            cmd.Parameters.AddWithValue("@classificacao", filme.Classificacao);
            cmd.Parameters.AddWithValue("@titulo", filme.Titulo);

            cmd.ExecuteNonQuery();
        }

        public void Remover(Filme filme)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Filmes 
                   WHERE Id = @id";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", filme.Id);

            cmd.ExecuteNonQuery();
        }
    }
}