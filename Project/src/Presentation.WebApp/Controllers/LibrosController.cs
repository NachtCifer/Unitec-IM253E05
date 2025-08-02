using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Domain;
using Infrastructure;
using Application;

namespace Presentation.WebApp.Controllers;

public class IM253E05LibrosController : Controller
{
    private readonly IM253E05LibrosDbContext _librosDbContext;
    
    public IM253E05LibrosController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty.");
        }
        _librosDbContext = new IM253E05LibrosDbContext(connectionString);
    }

    public IActionResult Index()
    {
        var libros = _librosDbContext.List();
        return View(libros);
    }

    public IActionResult Details(Guid id)
    { 
        var libro = _librosDbContext.Details(id);
        if (libro == null)
        {
            return NotFound();
        }
        return View(libro);
    }

    public IActionResult Create()
    {
        return View(new IM253E05Libro { Foto = FileConverterService.PlaceHolder });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(IM253E05Libro data, IFormFile file)
    {
        try
        {
            if (file != null && file.Length > 0)
            {
                data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
            }
            else
            {
                data.Foto = FileConverterService.PlaceHolder;
            }

            _librosDbContext.Create(data);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error al crear el libro: {ex.Message}");
            return View(data);
        }
    }

    public IActionResult Edit(Guid id)
    {
        var libro = _librosDbContext.Details(id);
        if (libro == null)
        {
            return NotFound();
        }
        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(IM253E05Libro data, IFormFile file, bool removeImage = false)
    {
        try
        {
            if (removeImage)
            {
                data.Foto = FileConverterService.PlaceHolder;
            }
            else if (file != null && file.Length > 0)
            {
                data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
            }

            _librosDbContext.Edit(data);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error al actualizar el libro: {ex.Message}");
            return View(data);
        }
    }

    public IActionResult Delete(Guid id)
    {
        _librosDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}