using System.Diagnostics;
using Presentation.WebApp.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IM253E05LibrosDbContext _librosDbContext;
        private readonly IM253E05UsuariosDbContext _usuariosDbContext;
        private readonly IM253E05PrestamosDbContext _prestamosDbContext;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string 'DefaultConnection' is not configured.");
            }

            _librosDbContext = new IM253E05LibrosDbContext(connectionString);
            _usuariosDbContext = new IM253E05UsuariosDbContext(connectionString);
            _prestamosDbContext = new IM253E05PrestamosDbContext(connectionString);
}
        public IActionResult Index()
        {
            var libros = _librosDbContext.List();
            var usuarios = _usuariosDbContext.List();
            var prestamos = _prestamosDbContext.List();
            var modelo = new HomeIndexViewModel
            {
                Libros = libros,
                Usuarios = usuarios,
                Prestamos = prestamos
            };
        

            return View(modelo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
