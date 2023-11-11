using TP9.Clases;
using System.Data.SQLite;

namespace TP9.Repositorios
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

        public void Create(int idTablero, Tarea task)
        {
            var query = $"INSERT INTO Tarea (Id, Id_tablero, Nombre, Estado, Descripcion, Color, Id_usuario_asignado)  VALUES (@Id, @idTablero, @Nombre, @Estado, @Descripcion, @Color, @IdUsuarioAsignado)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", task.Id));
                command.Parameters.Add(new SQLiteParameter("@IdTablero", idTablero));
                command.Parameters.Add(new SQLiteParameter("@Nombre", task.Nombre));
                command.Parameters.Add(new SQLiteParameter("@Estado", task.Estado));
                command.Parameters.Add(new SQLiteParameter("@Descripcion", task.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@Color", task.Color));
                command.Parameters.Add(new SQLiteParameter("@IdUsuarioAsignado", task.IdUsuarioAsignado));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Tarea task)
        {
            var query = "UPDATE Tarea SET Id_tablero = @Id_tablero, Nombre = @NombreTarea, Estado = @Estado,Descripcion = @Descripcion, Color = @Color, Id_usuario_asignado = @IdUsuario WHERE Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", id));
                command.Parameters.Add(new SQLiteParameter("@Id_tablero", task.Id_tablero));
                command.Parameters.Add(new SQLiteParameter("@NombreTarea", task.Nombre));
                command.Parameters.Add(new SQLiteParameter("@Estado", task.Estado));
                command.Parameters.Add(new SQLiteParameter("@Descripcion", task.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@Color", task.Color));
                command.Parameters.Add(new SQLiteParameter("@IdUsuario", task.IdUsuarioAsignado));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public Tarea GetById(int id)
        {
            var query = "SELECT Id, Id_tablero, Nombre, Estado, Descripcion, Color, Id_usuario_asignado FROM Tarea WHERE Id = @Id";
            var task = new Tarea();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@Id", id));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    task.Id = Convert.ToInt32(reader["Id"]);
                    task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                    task.Nombre = reader["Nombre"].ToString();
                    task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                    task.Descripcion = reader["Descripcion"].ToString();
                    task.Color = reader["Color"].ToString();
                    task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);
                }

                connection.Close();
            }
            return task;
        }

        public List<Tarea> GetByUsuario(int idUsuario)
        {
            var queryString = @"SELECT Id, Id_tablero, Nombre, Estado, Descripcion, Color, Id_usuario_propietario FROM Tarea WHERE Id_usuario_asignado = @idUsuario;";
            List<Tarea> Tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Tarea();
                        task.Id = Convert.ToInt32(reader["Id"]);
                        task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                        task.Nombre = reader["Nombre"].ToString();
                        task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                        task.Descripcion = reader["Descripcion"].ToString();
                        task.Color = reader["Color"].ToString();
                        task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);

                        Tareas.Add(task);
                    }
                }
                connection.Close();
            }
            return Tareas;
        }

        public List<Tarea> GetByTablero(int idTablero)
        {
            var queryString = @"SELECT Id, Id_tablero, Nombre, Estado, Descripcion, Color, Id_usuario_propietario FROM Tarea WHERE Id_usuario_asignado = @idTablero;";
            List<Tarea> Tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Tarea();
                        task.Id = Convert.ToInt32(reader["Id"]);
                        task.Id_tablero = Convert.ToInt32(reader["Id_tablero"]);
                        task.Nombre = reader["Nombre"].ToString();
                        task.Estado = (EstadoTarea)Convert.ToInt32(reader["Estado"]);
                        task.Descripcion = reader["Descripcion"].ToString();
                        task.Color = reader["Color"].ToString();
                        task.IdUsuarioAsignado = Convert.ToInt32(reader["Id_usuario_asignado"]);

                        Tareas.Add(task);
                    }
                }
                connection.Close();
            }
            return Tareas;
        }

        public void Remove(int id)
        {
            var query = "DELETE FROM Tarea WHERE Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AssignUserTask(int idUsuario, int idTarea)
        {
            var query = "UPDATE Tarea SET Id_usuario_asignado = @idUsuario WHERE Id = idTarea";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));

                command.ExecuteNonQuery();

                connection.Close();
            }

        }
    }
}