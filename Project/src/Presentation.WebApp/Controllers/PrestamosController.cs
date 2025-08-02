using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Domain;
using Application;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class IM253E05PrestamosController : Controller
{
    // Make sure IM253E05UsuariosDbContext exists in your infrastructure project under the correct namespace.
    private readonly IM253E05UsuariosDbContext _usuariosDbContext;
    private readonly IM253E05PrestamosDbContext _prestamosDbContext;
    private readonly IM253E05LibrosDbContext _librosDbContext;

    public IM253E05PrestamosController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty.");
        }

        _usuariosDbContext = new IM253E05UsuariosDbContext(connectionString);
        _prestamosDbContext = new IM253E05PrestamosDbContext(connectionString);
        _librosDbContext = new IM253E05LibrosDbContext(connectionString);
    }

    public IActionResult Index()
    {
        var prestamos = _prestamosDbContext.List();
        return View(prestamos);
    }

    public IActionResult Details(Guid id)
    {
        var data = _prestamosDbContext.Details(id);
        return View(data);
    }

    public IActionResult Create()
    {
        ViewBag.usuarios = _usuariosDbContext.List();
        ViewBag.Libros = _librosDbContext.List();
        return View(new IM253E05Prestamos { FechaPrestamo = DateTime.Now });
    }

    [HttpPost]
    public IActionResult Create(IM253E05Prestamos data)
    {
        _prestamosDbContext.Create(data);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var data = _prestamosDbContext.Details(id);
        ViewBag.usuarios = _usuariosDbContext.List();
        ViewBag.Libros = _librosDbContext.List();
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(IM253E05Prestamos data)
    {
        // If Update does not exist, use Edit or another appropriate method, or implement Update in your DbContext.
        _prestamosDbContext.Edit(data); // Replace 'Edit' with the actual method name if different
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _prestamosDbContext.Delete(id);
        return RedirectToAction("Index");
    }
    
}
