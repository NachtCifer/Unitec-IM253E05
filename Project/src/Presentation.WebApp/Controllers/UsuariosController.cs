
using Microsoft.AspNetCore.Mvc;

using Domain;
using Application;
using Infrastructure;

namespace Presentation.WebApp.Controllers;
public class IM253E05UsuariosController : Controller
{
    private readonly IM253E05UsuariosDbContext _usuariosDbContext;
    public IM253E05UsuariosController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty.");
        }
        _usuariosDbContext = new IM253E05UsuariosDbContext(connectionString);
    }

    public IActionResult Index()
    {
        var usuario = _usuariosDbContext.List();
        return View(usuario);
    }

    public IActionResult Details(Guid id)
    {
        var usuario = _usuariosDbContext.Details(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    public IActionResult Create()
    {
        return View(new IM253E05Usuario());
    }

    [HttpPost]
    public IActionResult Create(IM253E05Usuario usuario, IFormFile file)
    {
        _usuariosDbContext.Create(usuario);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var usuario = _usuariosDbContext.Details(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Edit(IM253E05Usuario data)
    {
        _usuariosDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _usuariosDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}