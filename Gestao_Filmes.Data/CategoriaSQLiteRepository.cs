using Microsoft.Data.Sqlite;
using Gestao_Filmes.Domain;
using System.Collections.Generic;

namespace Gestao_Filmes.Data
{
    public class CategoriaSQLiteRepository : ICategoriaRepository
    {
        private string _connectionString = "Data Source=filmes.db";

        public CategoriaSQLiteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Categorias
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL
            );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // os meus métodos:
        public void Adicionar(Categoria categoria)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Categorias (Nome)
                   VALUES (@nome)";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", categoria.Nome);

            cmd.ExecuteNonQuery();
        }

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome FROM Categorias";

            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Categoria
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                });
            }

            return lista;
        }

        public Categoria ProcurarPorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome 
                   FROM Categorias 
                   WHERE Nome = @nome COLLATE NOCASE";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", nome);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Categoria
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                };
            }

            return null;
        }

        public void Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Categorias WHERE Id = @id";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

    }
}