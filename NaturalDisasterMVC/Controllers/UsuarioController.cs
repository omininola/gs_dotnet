using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterMVC.Data;
using NaturalDisasterMVC.DTO;
using NaturalDisasterMVC.Models;

namespace NaturalDisasterMVC.Controllers;

public class UsuarioController : Controller
{
    private readonly AppDbContext _context;
    
    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Relatorios)
            .ToListAsync();
        return View(usuarios);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    public async Task<IActionResult> Edit(long id)
    {
        var usuario = await _context.Usuarios
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (usuario == null)
        {
            return NotFound();
        }

        var usuarioRequest = new UsuarioRequest
        {
            Nome = usuario.Nome,
            Email = usuario.Email,
            Senha = usuario.Senha,
        };

        return View(usuarioRequest);
    }
    
    public async Task<IActionResult> Delete(long id)
    {
        var usuario = await _context.Usuarios
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(UsuarioRequest usuarioRequest)
    {   
        if (ModelState.IsValid)
        {
            var usuario = new Usuario
            {
                Nome = usuarioRequest.Nome,
                Email = usuarioRequest.Email,
                Senha = usuarioRequest.Senha,
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        return View(usuarioRequest);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(long id, UsuarioRequest usuarioRequest)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cidades = await _context.Cidades.ToListAsync();
            return View(usuarioRequest);
        }

        var usuario = await _context.Usuarios
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        
        if (usuario == null)
        {
            return NotFound();
        }

        usuario.Nome = usuarioRequest.Nome;
        usuario.Email = usuarioRequest.Email;
        usuario.Senha = usuarioRequest.Senha;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Index));
    }
}