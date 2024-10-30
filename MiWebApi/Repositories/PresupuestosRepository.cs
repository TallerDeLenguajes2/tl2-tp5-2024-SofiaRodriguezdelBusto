
using Microsoft.Data.Sqlite;

class PresupuestosRepository
{
    private List<PresupuestoDetalle> ObtenerDetalleDelPresupuesto(int id)
    {
        ProductosRepository repoProducto = new ProductosRepository();
        List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";
        string queryDetalles = @"SELECT idProducto, Cantidad FROM PresupuestosDetalle WHERE idPresupuesto = @id";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(queryDetalles, connection);
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                //connection.Close();
                while (reader.Read())
                {
                    int idProducto = Convert.ToInt32(reader["idProducto"]);
                    int cantidad = Convert.ToInt32(reader["Cantidad"]);
                    Producto producto = repoProducto.ObtenerProductoPorId(idProducto);
                    PresupuestoDetalle detalle = new PresupuestoDetalle(producto, cantidad);
                    detalles.Add(detalle);
                }

            }

        }
        return detalles;
    }
    public List<Presupuesto> ObtenerPresupuestos()
    {
        List<Presupuesto> presupuestos = new List<Presupuesto>();
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"SELECT 
    P.idPresupuesto,
    P.NombreDestinatario,
    P.FechaCreacion,
    PR.idProducto,
    PR.Descripcion AS Producto,
    PR.Precio,
    PD.Cantidad,
    (PR.Precio * PD.Cantidad) AS Subtotal
FROM 
    Presupuestos P
JOIN 
    PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
JOIN 
    Productos PR ON PD.idProducto = PR.idProducto
ORDER BY P.idPresupuesto;";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                //connection.Close();
                int id = -1;
                Presupuesto ultPresupuesto = new Presupuesto(-1, " ");
                while (reader.Read())
                {
                    if(id == -1 || id != Convert.ToInt32(reader["idPresupuesto"]))
                    {
                        if (id != -1) presupuestos.Add(ultPresupuesto);
                        ultPresupuesto = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), reader["NombreDestinatario"].ToString());
                    }
                    Producto producto = new Producto(Convert.ToInt32(reader["idProducto"]), reader["Producto"].ToString(), Convert.ToInt32(reader["Precio"]));
                    PresupuestoDetalle detalle = new PresupuestoDetalle(producto,Convert.ToInt32(reader["Cantidad"]) );
                    ultPresupuesto.Detalle.Add(detalle);
                    id = Convert.ToInt32(reader["idPresupuesto"]);
                }
                presupuestos.Add(ultPresupuesto);

            }
            connection.Close();
        }
        return presupuestos;
    }


    public Presupuesto ObtenerPresupuestoPorId(int id)
    {
        Presupuesto presupuesto;
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"SELECT idPresupuesto, NombreDestinatario FROM Presupuestos WHERE idPresupuesto = @id";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                List<PresupuestoDetalle> detalles = ObtenerDetalleDelPresupuesto(Convert.ToInt32(reader["idPresupuesto"]));
                presupuesto = new Presupuesto(Convert.ToInt32(reader["idPresupuesto"]), reader["NombreDestinatario"].ToString());
                presupuesto.Detalle = detalles;

            }
            connection.Close();
        }
        return presupuesto;
    }

    public void AgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"INSERT INTO PresupuestoDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresu, @idProd, @cant)";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@idPresu", idPresupuesto);
            command.Parameters.AddWithValue("@idProd", idProducto);
            command.Parameters.AddWithValue("@cant", cantidad);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void EliminarPresupuestoPorId(int idPresupuesto)
    {
        string connectionString = @"Data Source = db/Tienda.db;Cache=Shared";

        string query = @"DELETE FROM Presupuestos WHERE idPresupuesto = @IdP;";
        string query2 = @"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @Id;";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteCommand command2 = new SqliteCommand(query2, connection);
            command.Parameters.AddWithValue("@IdP", idPresupuesto);
            command2.Parameters.AddWithValue("@Id", idPresupuesto);
            command2.ExecuteNonQuery();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }


}