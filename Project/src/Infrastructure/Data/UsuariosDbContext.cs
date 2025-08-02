using System.Data;
using Microsoft.Data.SqlClient;
using Domain;
using Application;

namespace Infrastructure;

public class IM253E05UsuariosDbContext
{
    private readonly string _connectionString = "Data Source=unitec-db.database.windows.net;Initial Catalog=im;User ID=AdminDB;Password=P@$$w0rd;";
    public IM253E05UsuariosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<IM253E05Usuario> List()
    {
        var data = new List<IM253E05Usuario>();
        var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM IM253E05Usuario", connection);
        try
        {
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new IM253E05Usuario
                {
                    Id = reader.GetGuid(0),
                    Nombre = reader.GetString(1),
                    Direccion = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Telefono = reader.GetString(3),
                    Correo = reader.GetString(4)
                });
            }
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving usuarios from database", ex);
        }
        finally
        {
            connection.Close();
        }
    }
    public IM253E05Usuario Details(Guid id)
    {
        //ToDo: Implement the method to retrieve details of a specific usuario by id.
        var data = new IM253E05Usuario();
        var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var command = new Microsoft.Data.SqlClient.SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM IM253E05Usuario WHERE Id = @Id", connection);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                data.Id = reader.GetGuid(0);
                data.Nombre = reader.GetString(1);
                data.Direccion = reader.IsDBNull(2) ? null : reader.GetString(2);
                data.Telefono = reader.GetString(3);
                data.Correo = reader.GetString(4);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving usuario from database", ex);
        }
        finally
        {
            connection.Close();
        }
        return data;
    }
    public void Create(IM253E05Usuario data)
    {
        //ToDo: Implement the method to create a new usuario in the database.
        var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var command = new Microsoft.Data.SqlClient.SqlCommand("INSERT INTO [IM253E05Usuario] ([Id], [Nombre], [Direccion], [Telefono], [Correo]) VALUES (@Id, @Nombre, @Direccion, @Telefono, @Correo)", connection);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = data.Nombre;
        command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = data.Direccion ?? (object)DBNull.Value;
        command.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = data.Telefono;
        command.Parameters.Add("@Correo", SqlDbType.NVarChar).Value = data.Correo;
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating usuario in database", ex);
        }
        finally
        {
            connection.Close();
        }

    }
    public void Edit(IM253E05Usuario data)
    {
        //ToDo: Implement the method to update an existing usuario in the database.
        var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var command = new Microsoft.Data.SqlClient.SqlCommand("UPDATE [IM253E05Usuario] SET [Nombre] = @Nombre, [Direccion] = @Direccion, [Telefono] = @Telefono, [Correo] = @Correo WHERE [Id] = @Id", connection);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        command.Parameters.Add("@Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        command.Parameters.Add("@Direccion", SqlDbType.NVarChar, 50).Value = data.Direccion ?? (object)DBNull.Value;
        command.Parameters.Add("@Telefono", SqlDbType.NVarChar, 15).Value = data.Telefono;
        command.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = data.Correo;
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating usuario in database", ex);
        }
        finally
        {
            connection.Close();
        }
        
    } 
    public void Delete(Guid id)
    {
        //ToDo: Implement the method to delete a usuario from the database.
        var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var command = new Microsoft.Data.SqlClient.SqlCommand("DELETE FROM [IM253E05Usuario] WHERE [Id] = @Id", connection);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting usuario from database", ex);
        }
        finally
        {
            connection.Close();
        }
    }
}