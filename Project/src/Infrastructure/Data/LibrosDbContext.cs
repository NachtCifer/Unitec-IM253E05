using Microsoft.Data.SqlClient;
using System.Data;
using Domain;
using Application;

namespace Infrastructure;

public class IM253E05LibrosDbContext
{
    private readonly string _connectionString;
    
    public IM253E05LibrosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<IM253E05Libro> List()
    {
        var data = new List<IM253E05Libro>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT [Id],[Autor],[Editorial],[ISBN],[Foto] FROM IM253E05Libro", connection);
        
        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                data.Add(new IM253E05Libro
                {
                    Id = reader.GetGuid(0),
                    Autor = reader.GetString(1),
                    Editorial = reader.IsDBNull(2) ? null : reader.GetString(2),
                    ISBN = reader.GetString(3),
                    Foto = reader.IsDBNull(4) ? FileConverterService.PlaceHolder : EnsureImagePrefix(reader.GetString(4))
                });
            }
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving libros from database", ex);
        }
    }

    public IM253E05Libro Details(Guid id)
    {
        var data = new IM253E05Libro();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT [Id],[Autor],[Editorial],[ISBN],[Foto] FROM IM253E05Libro WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        
        try
        {
            connection.Open();
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                data.Id = reader.GetGuid(0);
                data.Autor = reader.GetString(1);
                data.Editorial = reader.IsDBNull(2) ? null : reader.GetString(2);
                data.ISBN = reader.GetString(3);
                data.Foto = reader.IsDBNull(4) ? FileConverterService.PlaceHolder : EnsureImagePrefix(reader.GetString(4));
            }
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving libro details from database", ex);
        }
    }

    public void Create(IM253E05Libro data)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            "INSERT INTO [IM253E05Libro] ([Id],[Autor],[Editorial],[ISBN],[Foto]) " +
            "VALUES (@Id, @Autor, @Editorial, @ISBN, @Foto)", connection);
        
        command.Parameters.AddWithValue("@Id", Guid.NewGuid());
        command.Parameters.AddWithValue("@Autor", data.Autor);
        command.Parameters.AddWithValue("@Editorial", data.Editorial ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ISBN", data.ISBN);
        command.Parameters.AddWithValue("@Foto", 
            data.Foto != FileConverterService.PlaceHolder ? (object)data.Foto : DBNull.Value);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating libro in database", ex);
        }
    }

    public void Edit(IM253E05Libro data)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            "UPDATE [IM253E05Libro] SET [Autor] = @Autor, [Editorial] = @Editorial, " +
            "[ISBN] = @ISBN, [Foto] = @Foto WHERE [Id] = @Id", connection);
        
        command.Parameters.AddWithValue("@Id", data.Id);
        command.Parameters.AddWithValue("@Autor", data.Autor);
        command.Parameters.AddWithValue("@Editorial", data.Editorial ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ISBN", data.ISBN);
        command.Parameters.AddWithValue("@Foto", 
            data.Foto != FileConverterService.PlaceHolder ? (object)data.Foto : DBNull.Value);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating libro in database", ex);
        }
    }

    public void Delete(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("DELETE FROM [IM253E05Libro] WHERE [Id] = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting libro from database", ex);
        }
    }

    private string EnsureImagePrefix(string imageData)
    {
        if (string.IsNullOrEmpty(imageData) || imageData.StartsWith("data:image"))
            return imageData;
            
        return $"data:image/png;base64,{imageData}";
    }
}