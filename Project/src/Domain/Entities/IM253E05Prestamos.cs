/*
CREATE TABLE [IM253E05Prestamos] (
    [Id] [uniqueidentifier] NOT NULL,
    [UsuarioId] [uniqueidentifier] NOT NULL,
    [LibroId] [uniqueidentifier] NOT NULL,
    [FechaPrestamo] [smalldatetime] NOT NULL,
    [FechaDevolucion] [smalldatetime] NULL,

    CONSTRAINT PK_IM253E05Prestamos PRIMARY KEY ([Id]),
    CONSTRAINT FK_IM253E05Prestamos_IM253E05Usuario FOREIGN KEY ([UsuarioId]) REFERENCES [IM253E05Usuario] ([Id]),
    CONSTRAINT FK_IM253E05Prestamos_IM253E05Libro FOREIGN KEY ([LibroId]) REFERENCES [IM253E05Libro] ([Id])
);
*/

namespace Domain;
public class IM253E05Prestamos
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid LibroId { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }

    
    public IM253E05Usuario? Usuario { get; set; }
    public IM253E05Libro? Libro { get; set; }


}

