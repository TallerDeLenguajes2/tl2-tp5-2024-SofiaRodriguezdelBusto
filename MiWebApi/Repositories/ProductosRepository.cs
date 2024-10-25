
using Microsoft.Data.Sqlite;

class ProductosRepository
{
    public void CrearProducto(Producto producto)
    {
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"INSERT INTO Productos (Descripcion, Precio) 
        VALUES (@Descripcion, @Precio)";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);
            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.ExecuteNonQuery();
            connection.Close();            
        }

    }

    public List<Producto>  ObtenerProductos()
    {
        List<Producto> productos = new List<Producto>();
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"SELECT * FROM Productos";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Producto nuevoProducto = new Producto();
                    nuevoProducto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    nuevoProducto.Descripcion = reader["Descripcion"].ToString();
                    nuevoProducto.Precio = Convert.ToInt32(reader["Precio"]);
                    productos.Add(nuevoProducto);

                }
                
            }
            connection.Close();            
        }

        return productos;

    }

    public void ModificarProducto(int id, Producto producto)
    {
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @Id";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query,connection);
            command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
            command.Parameters.AddWithValue("@Precio", producto.Precio);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            connection.Close();            
        }

    }
    

}