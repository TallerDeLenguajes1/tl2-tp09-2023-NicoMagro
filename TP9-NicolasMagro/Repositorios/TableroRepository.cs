using TP9.Clases;
using System.Data.SQLite;

namespace TP9.Repositorios
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

        public void Create(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (Id, Id_usuario_propietario, Nombre, Descripcion) VALUES (@Id, @Id_usuarioPropietario, @Nombre, @Descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", tablero.Id));
                command.Parameters.Add(new SQLiteParameter("@Id_usuario_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@Nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@Descripcion", tablero.Descripcion));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Tablero tablero)
        {
            var query = "UPDATE Tablero SET Id_usuario_propietario = @Id_usuario, Nombre = @Nombre_tablero, Descripcion = @Descripcion WHERE Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", id));
                command.Parameters.Add(new SQLiteParameter("@Id_usuario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@Nombre_tablero", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@Descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> Tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["Id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["Id_usuario_propietario"]);
                        tablero.Nombre = reader["Nombre"].ToString();
                        tablero.Descripcion = reader["Descripcion"].ToString();
                        Tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return Tableros;
        }

        public Tablero GetById(int id)
        {
            var query = "SELECT Id, Id_usuario_propietario, Nombre, Descripcion FROM Tablero WHERE Id = @Id";
            var tablero = new Tablero();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@Id", id));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    tablero.Id = Convert.ToInt32(reader["Id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["Id_usuario_propietario"]);
                    tablero.Nombre = reader["Nombre"].ToString();
                    tablero.Descripcion = reader["Descripcion"].ToString();
                }

                connection.Close();
            }
            return tablero;
        }

        public void Remove(int id)
        {
            var query = "DELETE FROM Tablero WHERE Id = @Id";

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