using Microsoft.Data.Sqlite;
using espacioPresupuestos;

public class PresupuestosRepository
{
    private string connectionString = "Data Source=DB/Tienda_final.db;";

    public int Alta(Presupuestos presupuesto)
    {
        int nuevoId = 0;
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombre, @fecha); SELECT last_insert_rowid();";
            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@nombre", presupuesto.NombreDestinatario);
                command.Parameters.AddWithValue("@fecha", presupuesto.FechaCreacion);
                nuevoId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return nuevoId;
    }

    public List<Presupuestos> GetAll()
    {
        List<Presupuestos> listaPresupuestos = new List<Presupuestos>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT idPresupuesto, NombreDestinatario, FechaCreacion FROM Presupuestos";

            using (var command = new SqliteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaPresupuestos.Add(new Presupuestos
                    {

                        IdPresupuesto = reader.GetInt32(reader.GetOrdinal("idPresupuesto")),
                        NombreDestinatario = reader.GetString(reader.GetOrdinal("NombreDestinatario")),
                        FechaCreacion = DateOnly.Parse(reader.GetString(reader.GetOrdinal("FechaCreacion")))
                    });
                }
            }
        }
        return listaPresupuestos;
    }

    public int Baja(int id)
    {
        int resultado;
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string sql = "DELETE FROM Presupuestos WHERE idPresupuesto = @identificador";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@identificador", id);
                resultado = command.ExecuteNonQuery();
            }

        }
        return resultado;
    }

    public Presupuestos Detalles(int id)
    {
        Presupuestos presupuesto = new Presupuestos();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT * FROM Presupuestos WHERE idPresupuesto = @identificador";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@identificador", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        presupuesto.NombreDestinatario = reader.GetString(reader.GetOrdinal("NombreDestinatario"));
                        presupuesto.FechaCreacion = DateOnly.Parse(reader.GetString(reader.GetOrdinal("FechaCreacion")));
                    }
                }
            }
        }
        return presupuesto;
    }

    public void AgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string sql = @"INSERT INTO PresupuestosDetalle
                           (idPresupuesto, idProducto, Cantidad)
                           VALUES (@pres, @prod, @cant)";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@pres", idPresupuesto);
                command.Parameters.AddWithValue("@prod", idProducto);
                command.Parameters.AddWithValue("@cant", cantidad);

                command.ExecuteNonQuery();
            }
        }
    }
}