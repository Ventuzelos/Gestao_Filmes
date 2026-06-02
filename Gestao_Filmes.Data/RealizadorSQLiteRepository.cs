using Microsoft.Data.Sqlite;
using Gestao_Filmes.Domain;
using System.Collections.Generic;

namespace Gestao_Filmes.Data
{
    public class RealizadorSQLiteRepository : IRealizadorRepository
    {
        private string _connectionString = "Data Source=filmes.db";

        public RealizadorSQLiteRepository()
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Realizadores
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Pais TEXT NOT NULL
            );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // os meus métodos:
        public void Adicionar(Realizador realizador)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"INSERT INTO Realizadores (Nome, Pais)
                   VALUES (@nome, @pais)";

            using var cmd = new SqliteCommand(sql, con);

            cmd.Parameters.AddWithValue("@nome", realizador.Nome);
            cmd.Parameters.AddWithValue("@pais", realizador.Pais);

            cmd.ExecuteNonQuery();
        }

        public List<Realizador> Listar()
        {
            List<Realizador> lista = new List<Realizador>();

            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome, Pais FROM Realizadores";

            using var cmd = new SqliteCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Realizador
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2)
                });
            }

            return lista;
        }

        public Realizador ProcurarPorNome(string nome)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"SELECT Id, Nome, Pais
                   FROM Realizadores
                   WHERE Nome = @nome COLLATE NOCASE";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@nome", nome);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Realizador
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2)
                };
            }

            return null;
        }

        public void Remover(int id)
        {
            using var con = new SqliteConnection(_connectionString);
            con.Open();

            string sql = @"DELETE FROM Realizadores WHERE Id = @id";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}