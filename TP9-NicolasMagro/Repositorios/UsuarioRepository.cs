using Microsoft.AspNetCore.DataProtection.Repositories;
using TP9.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TP9.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

        public void Create(Usuario user)
        {
            var query = $"INSERT INTO Usuario (Id, Nombre_de_usuario) VALUES (@Id,@Nombre)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", user.Id));
                command.Parameters.Add(new SQLiteParameter("@Nombre", user.Nombre));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Usuario user)
        {
            var query = "UPDATE Usuario SET Nombre_de_usuario = @Nombre WHERE Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", id));
                command.Parameters.Add(new SQLiteParameter("@Nombre", user.Nombre));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> Usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new Usuario();
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.Nombre = reader["Nombre"].ToString();
                        Usuarios.Add(user);
                    }
                }
                connection.Close();
            }
            return Usuarios;
        }

        public Usuario GetById(int id)
        {
            var query = "SELECT Id, Nombre_de_usuario FROM Usuario WHERE Id = @Id";
            var user = new Usuario();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.Nombre = reader["Nombre"].ToString();
                }

                connection.Close();
            }
            return user;
        }

        public void Remove(int id)
        {
            var query = "DELETE FROM Usuario WHERE Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
}