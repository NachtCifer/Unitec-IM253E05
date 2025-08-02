using System.Collections.Generic;
using Domain;

namespace Presentation.WebApp.Models
{
    public class HomeIndexViewModel
    {
        public List<IM253E05Libro> Libros { get; set; } = new();
        public List<IM253E05Usuario> Usuarios { get; set; } = new();
        public List<IM253E05Prestamos> Prestamos { get; set; } = new();
    }
}
