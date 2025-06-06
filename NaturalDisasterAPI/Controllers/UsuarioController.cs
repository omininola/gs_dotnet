using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalDisasterAPI.Data;
using NaturalDisasterAPI.DTO;
using NaturalDisasterAPI.DTO.RelatorioDTO;
using NaturalDisasterAPI.DTO.UsuarioDTO;
using NaturalDisasterAPI.Models;

namespace NaturalDisasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> PostUsuario([FromBody] UsuarioRequest usuarioRequest)
        {
            var usuario = new Usuario
            {
                Nome = usuarioRequest.Nome,
                Email = usuarioRequest.Email,
                Senha = usuarioRequest.Senha,
            };
            
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetUsuario), new { Id = usuario.Id }, usuario);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Relatorios)
                .Select(u => new UsuarioResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Relatorios = u.Relatorios.Select(r => new RelatorioResponse
                    {
                        Id = r.Id,
                        CidadeNome = r.Cidade.Nome,
                        Descricao = r.Descricao,
                        Data = r.Data,
                    }).ToList()
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuario(long id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Relatorios)
                .Where(u => u.Id == id)
                .Select(u => new UsuarioResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Relatorios = u.Relatorios.Select(r => new RelatorioResponse
                    {
                        Id = r.Id,
                        CidadeNome = r.Cidade.Nome,
                        Descricao = r.Descricao,
                        Data = r.Data,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioResponse>> PutUsuario(long id, [FromBody] UsuarioRequest usuarioRequest)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }

            usuario.Nome = usuarioRequest.Nome;
            usuario.Email = usuarioRequest.Email;
            usuario.Senha = usuarioRequest.Senha;

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var usuarioResponse = new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            };
            
            return Ok(usuarioResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
