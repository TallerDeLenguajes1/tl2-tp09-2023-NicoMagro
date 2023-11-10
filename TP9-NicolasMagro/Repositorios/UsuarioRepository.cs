namespace TP9.Repositorios
{
    public class UsuarioRepository : IRepository
    {
        private string cadenaConexion = "Data Source=TP9/Kanban.db;Cache=Shared";

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
                        user.Name = reader["Nombre"].ToString();
                        Usuarios.Add(user);
                    }
                }
                connection.Close();
            }
            return Usuarios;
        }

    }
}