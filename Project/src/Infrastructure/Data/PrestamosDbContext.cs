
using Microsoft.Data.SqlClient;
using System.Data;
using Domain;
using Application;


namespace Infrastructure;
public class IM253E05PrestamosDbContext
{
    private readonly string _connectionString = "Data Source=unitec-db.database.windows.net;Initial Catalog=im;User ID=AdminDB;Password=P@$$w0rd;";

    public IM253E05PrestamosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<IM253E05Prestamos> List()
    {
        var data = new List<IM253E05Prestamos>();
        using var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var cmd = new SqlCommand(@"
            SELECT p.[Id], p.[UsuarioId], p.[LibroId], p.[FechaPrestamo], p.[FechaDevolucion],
            u.[Nombre] AS NombreUsuario,
            l.[Autor] AS NombreLibro
            FROM [IM253E05Prestamos] p
            JOIN [IM253E05Usuario] u ON p.[UsuarioId] = u.[Id]
            JOIN [IM253E05Libro] l ON p.[LibroId] = l.[Id]", con);

        con.Open();
        var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            data.Add(new IM253E05Prestamos
            {
                Id = (Guid)dr["Id"],
                UsuarioId = (Guid)dr["UsuarioId"],
                LibroId = (Guid)dr["LibroId"],
                FechaPrestamo = (DateTime)dr["FechaPrestamo"],
                FechaDevolucion = dr["FechaDevolucion"] as DateTime?,
                Usuario = new IM253E05Usuario { Nombre = (string)dr["NombreUsuario"] },
                Libro = new IM253E05Libro { Autor = (string)dr["NombreLibro"] }
            });
        }
        return data;
    }

    public IM253E05Prestamos Details(Guid id)
    {
        using var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
            SELECT p.Id, p.UsuarioId, p.LibroId, p.FechaPrestamo, p.FechaDevolucion,
            u.Nombre AS NombreUsuario,
            l.Autor AS NombreLibro
            FROM IM253E05Prestamos p
            JOIN IM253E05Usuario u ON p.UsuarioId = u.Id
            JOIN IM253E05Libro l ON p.LibroId = l.Id
            WHERE p.Id = @Id", con);

        cmd.Parameters.AddWithValue("@Id", id);
        con.Open();
        var dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            return new IM253E05Prestamos
            {
                Id = (Guid)dr["Id"],
                UsuarioId = (Guid)dr["UsuarioId"],
                LibroId = (Guid)dr["LibroId"],
                FechaPrestamo = (DateTime)dr["FechaPrestamo"],
                FechaDevolucion = dr["FechaDevolucion"] as DateTime?,
                Usuario = new IM253E05Usuario { Nombre = (string)dr["NombreUsuario"] },
                Libro = new IM253E05Libro { Autor = (string)dr["NombreLibro"] }
            };
        }
        throw new Exception("Préstamo no encontrado");
    }

    public void Create(IM253E05Prestamos data)
    {
        data.Id = Guid.NewGuid();
        using var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
            INSERT INTO IM253E05Prestamos (Id, UsuarioId, LibroId, FechaPrestamo, FechaDevolucion)
            VALUES (@Id, @UsuarioId, @LibroId, @FechaPrestamo, @FechaDevolucion)", con);

        cmd.Parameters.AddWithValue("@Id", data.Id);
        cmd.Parameters.AddWithValue("@UsuarioId", data.UsuarioId);
        cmd.Parameters.AddWithValue("@LibroId", data.LibroId);
        cmd.Parameters.AddWithValue("@FechaPrestamo", data.FechaPrestamo);
        cmd.Parameters.AddWithValue("@FechaDevolucion", (object?)data.FechaDevolucion ?? DBNull.Value);

        con.Open();
        cmd.ExecuteNonQuery();
    }
    public void Edit(IM253E05Prestamos data)
    {
        using var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
            UPDATE IM253E05Prestamos
            SET UsuarioId = @UsuarioId, LibroId = @LibroId, FechaPrestamo = @FechaPrestamo, FechaDevolucion = @FechaDevolucion
            WHERE Id = @Id", con);

        cmd.Parameters.AddWithValue("@Id", data.Id);
        cmd.Parameters.AddWithValue("@UsuarioId", data.UsuarioId);
        cmd.Parameters.AddWithValue("@LibroId", data.LibroId);
        cmd.Parameters.AddWithValue("@FechaPrestamo", data.FechaPrestamo);
        cmd.Parameters.AddWithValue("@FechaDevolucion", (object?)data.FechaDevolucion ?? DBNull.Value);

        con.Open();
        cmd.ExecuteNonQuery();
    }
    public void Delete(Guid id)
    {
        using var con = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        var cmd = new Microsoft.Data.SqlClient.SqlCommand("DELETE FROM IM253E05Prestamos WHERE Id = @Id", con);
        cmd.Parameters.AddWithValue("@Id", id);
        con.Open();
        cmd.ExecuteNonQuery();
    }
}